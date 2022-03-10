using WebApiAutores.Validaciones;

namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int id { get; set; }
        [PrimeraLetraMayuscula]
        public string Titulo { get; set; }
        public int Autorid { get; set; }
        public Autor Autor { get; set; }
    }
}
