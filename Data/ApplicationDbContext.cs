using Microsoft.EntityFrameworkCore;
using ClientesAPI.Models;

namespace ClientesAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Servicio> Servicios { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ClienteId);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(e => e.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(e => e.Email)        
                      .IsUnique();

                entity.Property(e => e.Telefono)
                      .HasMaxLength(20);

                entity.Property(e => e.Direccion)
                      .HasMaxLength(200);

                entity.Property(e => e.FechaCreacion)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Activo)
                      .HasDefaultValue(true);

                // relación 1 Cliente : N Reservas
                entity.HasMany(e => e.Reservas)
                      .WithOne(r => r.Cliente)
                      .HasForeignKey(r => r.ClienteId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Servicio>(entity =>
            {
                entity.HasKey(e => e.ServicioId);

                entity.Property(e => e.Nombre)
                      .IsRequired()
                      .HasMaxLength(120);

                entity.Property(e => e.Descripcion)
                      .HasMaxLength(300);

                entity.Property(e => e.FechaCreacion)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(e => e.Activo)
                      .HasDefaultValue(true);

                // relación 1 Servicio : N Reservas
                entity.HasMany(e => e.Reservas)
                      .WithOne(r => r.Servicio)
                      .HasForeignKey(r => r.ServicioId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Reserva>(entity =>
            {
                entity.HasKey(e => e.ReservaId);

                entity.Property(e => e.Estado)
                      .HasMaxLength(20)
                      .HasDefaultValue("Pendiente");

                entity.Property(e => e.Descripcion)
                      .HasMaxLength(500);
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.HasKey(u => u.UsuarioId);

                entity.Property(u => u.Nombre)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasIndex(u => u.Email)         
                      .IsUnique();

                entity.Property(u => u.PasswordHash)
                      .IsRequired();
            });
        }
    }
}
