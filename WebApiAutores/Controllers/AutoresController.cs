using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;
using WebApiAutores.Servicios;

namespace WebApiAutores.Controllers
{
    // GET: AutoresController
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {

        private readonly ApplicationDbContext context;
        private readonly Iservicio servicio;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioSingleton servicioSingleton;
        private readonly ServicioScoped servicioScoped;

        public AutoresController(ApplicationDbContext context, Iservicio servicio , ServicioTransient servicioTransient, 
            ServicioSingleton servicioSingleton , ServicioScoped servicioScoped)
        {
            //context.Database.EnsureCreated();
            this.context = context;
            this.servicio = servicio;
            this.servicioTransient = servicioTransient;
            this.servicioSingleton = servicioSingleton;
            this.servicioScoped = servicioScoped;
        }


        [HttpGet("GUID")]   
        public  ActionResult ObtenerGuids()
        {
            return Ok(new
            {
                AutoresController_Transient = servicioTransient.guid,
                ServicioA_Transient = servicio.ObtenerTransient(),
                AutoresController_Scoped = servicioScoped.guid,
                ServicioA_Scoped = servicio.ObtenerScoped(),
                AutoresController_Singleton = servicioSingleton.guid,             
                ServicioA_Singleton = servicio.ObtenerSingleton()
            }) ;
            
        }

        [HttpGet]// api/autores
        [HttpGet("listado")] // api/autores/listado
        [HttpGet("/listado")] // listadop
        public async Task<ActionResult<List<Autor>>> Get()
        {

            servicio.RealizarTarea();
            return await context.Autores.Include(x => x.Libros).ToListAsync();
        }
        [HttpGet("primero")]//api/autores/primero
        public async Task<ActionResult<Autor>> PrimerAutor([FromHeader] int miValor, [FromQuery] string nombre)
        {
            return await context.Autores.FirstOrDefaultAsync();
        }
        [HttpGet("primero2")]//api/autores/primero
        public  ActionResult<Autor> PrimerAutor2()
        {
            return new Autor() {Nombre = "inventado"};
        }

        [HttpGet("{id:int}/{param2=persona}")]
        public async Task<ActionResult<Autor>> Get(int id, string param2)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Id == id);

            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }
        [HttpGet("{nombre}")]
        public async Task<ActionResult<Autor>> Get([FromRoute]string nombre)
        {
            var autor = await context.Autores.FirstOrDefaultAsync(x => x.Nombre.Contains(nombre));

            if (autor == null)
            {
                return NotFound();
            }
            return autor;
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Autor autor)
        {
            var existeAutorConElMismoNomre = await context.Autores.AnyAsync(x => x.Nombre == autor.Nombre);
            if (existeAutorConElMismoNomre)
            {
                return BadRequest($"Ya existe un autor con el nombre {autor.Nombre}");
            }
            context.Add(autor);
            await context.SaveChangesAsync();
            return Ok();
        }


        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Autor autor, int id)
        {
            if (autor == null) { return BadRequest("el id esta vacio"); }
            if (autor.Id != id) { return BadRequest("el id esta vacio"); }

            context.Update(autor);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Autores.AnyAsync(x => x.Id == id);

            if (!existe)
            {
                return NotFound();
            }

            context.Remove(new Autor { Id = id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}
