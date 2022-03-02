namespace WebApiAutores.Entidades
{
    public class Libro
    {
        public int id { get; set; }
        public string Titulo { get; set; }
        public int Autorid { get; set; }
        public Autor Autor { get; set; }
    }
}
