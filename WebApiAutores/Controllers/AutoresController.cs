﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiAutores.Entidades;

namespace WebApiAutores.Controllers
{
    // GET: AutoresController
    [ApiController]
    [Route("api/autores")]
    public class AutoresController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AutoresController(ApplicationDbContext context)
        {
            //context.Database.EnsureCreated();
            this.context = context;
        }

        [HttpGet]// api/autores
        [HttpGet("listado")]
        [HttpGet("/listado")]
        public async Task<List<Autor>> Get()
        {
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
