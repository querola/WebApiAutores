namespace WebApiAutores.Servicios
{
    public interface Iservicio
    {
        void RealizarTarea();

    }

    public class ServicioA : Iservicio
    {
        private readonly ILogger<ServicioA> logger;

        public ServicioA (ILogger<ServicioA> logger)
        {
            this.logger = logger; 
        }
        public void RealizarTarea()
        {
   
        }
    }
    public class ServicioB : Iservicio
    {
        public void RealizarTarea()
        {
           
        }
    }
}
