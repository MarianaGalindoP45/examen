using examen.Models;
using Microsoft.EntityFrameworkCore;

namespace examen.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options)
            : base(options)
        {
            
        }
        //DB sets
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<SalidaInventario> SalidasInventario { get; set; }
        public DbSet<EntradaInventario> EntradasInventario { get; set; }


    }
}
