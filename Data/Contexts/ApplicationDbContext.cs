using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Data.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Herramientas> Herramienta { get; set; }
        public DbSet<Instalaciones> Instalacion { get; set; }
        public DbSet<Reportes> Reportes { get; set; }
        public DbSet<ReservacionHerramientas> ReservasHerramientas { get; set; }
        public DbSet<ReservacionInstalaciones> ReservasInstalaciones { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relaciones y restricciones
             // Relación Usuario -> ReservasHerramientas (1 a muchos)
            modelBuilder.Entity<ReservacionHerramientas>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Herramienta -> ReservasHerramientas (1 a muchos)
            modelBuilder.Entity<ReservacionHerramientas>()
                .HasOne<Herramientas>()
                .WithMany()
                .HasForeignKey(r => r.HerramientaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuario -> ReservasInstalaciones (1 a muchos)
            modelBuilder.Entity<ReservacionInstalaciones>()
                .HasOne<Usuario>()
                .WithMany()
                .HasForeignKey(r => r.UsuarioId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Instalacion -> ReservasInstalaciones (1 a muchos)
            modelBuilder.Entity<ReservacionInstalaciones>()
                .HasOne<Instalaciones>()
                .WithMany()
                .HasForeignKey(r => r.InstalacionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    
    }
}
