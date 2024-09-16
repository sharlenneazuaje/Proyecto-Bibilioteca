namespace BibliotecaPNT.Models
{
    public class LibroSearchViewModel
    {
        public string? titulo { get; set; }
        public string? autor { get; set; }
        public int? año { get; set; }
        public Genero? genero { get; set; }
        public string? prestatario { get; set; }
        public List<Libro> libros { get; set; }
    }
}
