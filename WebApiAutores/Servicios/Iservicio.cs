namespace WebApiAutores.Servicios
{
    public interface Iservicio
    {
        Guid ObtenerScoped();
        Guid ObtenerSingleton();
        Guid ObtenerTransient();
        void RealizarTarea();

    }

    public class ServicioA : Iservicio
    {
        private readonly ILogger<ServicioA> logger;
        private readonly ServicioTransient servicioTransient;
        private readonly ServicioScoped servicioScoped;
        private readonly ServicioSingleton servicioSingleton;

        public ServicioA (ILogger<ServicioA> logger , ServicioTransient servicioTransient, 
            ServicioScoped servicioScoped, ServicioSingleton servicioSingleton)
        {
            this.logger = logger;
            this.servicioTransient = servicioTransient;
            this.servicioScoped = servicioScoped;
            this.servicioSingleton = servicioSingleton;
        }

        public Guid ObtenerTransient() { return servicioTransient.guid; }
        public Guid ObtenerScoped() { return servicioScoped.guid; }
        public Guid ObtenerSingleton() { return servicioSingleton.guid; }


        public void RealizarTarea()
        {
   
        }
    }
    public class ServicioB : Iservicio
    {
        public Guid ObtenerScoped()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerSingleton()
        {
            throw new NotImplementedException();
        }

        public Guid ObtenerTransient()
        {
            throw new NotImplementedException();
        }

        public void RealizarTarea()
        {
           
        }
    }

    public class ServicioTransient
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServicioScoped
    {
        public Guid guid = Guid.NewGuid();
    }

    public class ServicioSingleton
    {
        public Guid guid = Guid.NewGuid();
    }
}
