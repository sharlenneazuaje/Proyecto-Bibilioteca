using BibliotecaPNT.Models;
using Microsoft.EntityFrameworkCore;

namespace BibliotecaPNT.Context
{
    public class BibliotecaContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=LAPTOP-3TGF6ERD\\SQLEXPRESS; Initial Catalog=BibliotecaDB; Integrated Security=true; Encrypt=True;TrustServerCertificate=True");
        }

        public DbSet<Libro> libros { get; set; }
    }
}
