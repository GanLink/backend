using GanLink.FarmManagement.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GanLink.FarmManagement.Infraestructure.Persistence.EF.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyFarmingConfiguration(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Farm>(ConfigureFarm);
        }

        private static void ConfigureFarm(EntityTypeBuilder<Farm> b)
        {
            b.ToTable("Farms");

            b.HasKey(x => x.Id);
            b.Property(x => x.Id).ValueGeneratedOnAdd();

            b.Property(x => x.Alias)
                .IsRequired()
                .HasMaxLength(120);
            
            b.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);

            b.Property(x => x.UserId)
                .IsRequired();
            
            // --- NUEVO ---
            // Configuración para la URL de la imagen
            b.Property(x => x.ImageUrl)
                .IsRequired(false) // Es opcional (nullable)
                .HasMaxLength(1024); // Longitud para una URL larga

            // Mapear enum Activity (CARNE, LECHE, GENERICA)
            // Usa string para legibilidad o int si prefieres compacidad.
            b.Property(x => x.MainActivity)
                .IsRequired()
                .HasConversion<string>()   // o .HasConversion<int>()
                .HasMaxLength(16);

            b.Property(x => x.OwnerDni)
                .IsRequired()
                .HasMaxLength(8);

            // Índice útil: evita alias duplicados por usuario
            b.HasIndex(x => new { x.UserId, x.Alias })
                .IsUnique();

            // NO configures navegación a User aquí (rompe bounded contexts).
            // Si más adelante decides compartir DbContext (no recomendado),
            // recién allí podrías configurar FK explícita a IAM.User.
        }
    }
}