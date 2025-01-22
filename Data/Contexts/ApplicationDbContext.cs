using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Models;
using Microsoft.EntityFrameworkCore;


namespace Data.Contexts
{
    public partial class ApplicationDbContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Herramienta> Herramienta { get; set; }
        public DbSet<Instalacion> Instalacion { get; set; }
        public DbSet<Reporte> Reportes { get; set; }
        public DbSet<ReservacionHerramienta> ReservasHerramientas { get; set; }
        public DbSet<ReservacionInstalacion> ReservasInstalaciones { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configurar relaciones y restricciones
             // Relación Usuario -> ReservasHerramientas (1 a muchos)
            modelBuilder.Entity<ReservacionHerramienta>()
                .HasOne(r => r.Usuario) // Propiedad de navegación
                .WithMany(u => u.ReservacionHerramientas) // Relación inversa
                .HasForeignKey(r => r.Usuario_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Herramienta -> ReservasHerramientas (1 a muchos)
            modelBuilder.Entity<ReservacionHerramienta>()
                .HasOne(r => r.Herramienta) // Propiedad de navegación
                .WithMany(h => h.ReservacionHerramientas) // Relación inversa
                .HasForeignKey(r => r.Herramienta_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Usuario -> ReservasInstalaciones (1 a muchos)
            modelBuilder.Entity<ReservacionInstalacion>()
                .HasOne(r => r.Usuario) // Propiedad de navegación
                .WithMany(u => u.ReservacionInstalaciones) // Relación inversa
                .HasForeignKey(r => r.Usuario_ID)
                .OnDelete(DeleteBehavior.Restrict);

            // Relación Instalacion -> ReservasInstalaciones (1 a muchos)
            modelBuilder.Entity<ReservacionInstalacion>()
                .HasOne(r => r.Instalacion) // Propiedad de navegación
                .WithMany(i => i.ReservacionInstalaciones) // Relación inversa
                .HasForeignKey(r => r.Instalacion_ID)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    
    }
}
