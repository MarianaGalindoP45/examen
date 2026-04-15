using examen.Models;
using Microsoft.EntityFrameworkCore;

namespace examen.Base
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Producto> Productos { get; set; }
        public DbSet<SalidaInventario> SalidasInventario { get; set; }
        public DbSet<EntradaInventario> EntradasInventario { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasIndex(u => u.Correo).IsUnique();
                entity.Property(u => u.Correo).HasMaxLength(150);
                entity.Property(u => u.ContrasenaHash).HasMaxLength(500);
            });
        }
    }
}
