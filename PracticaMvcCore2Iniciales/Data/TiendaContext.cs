using Microsoft.EntityFrameworkCore;
using PracticaMvcCore2Iniciales.Models;

namespace PracticaMvcCore2Iniciales.Data
{
    public class TiendaContext : DbContext
    {
        public TiendaContext
            (DbContextOptions<TiendaContext> options)
                : base(options) { }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
    }
}
