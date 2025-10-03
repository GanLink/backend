using Agg = GanLink.BovinueSystem.Domain.Models.Aggregates;
using Ent = GanLink.BovinueSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyBovinueSystemConfiguration(this ModelBuilder modelBuilder)
        {
            // ================
            // Bovinue (Agg)
            // ================
            modelBuilder.Entity<Agg.Bovinue>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();

                /*b.HasOne(x => x.farm)
                 .WithMany()
                 .HasForeignKey(x => x.FarmId)
                 .OnDelete(DeleteBehavior.Restrict);
*/
                b.HasQueryFilter(x => !x.deleted);
            });

            // =========================
            // Catálogo de métricas (Ent)
            // =========================
            modelBuilder.Entity<Agg.BovinueMetricCategory>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Category).HasConversion<string>();

                b.HasMany(c => c.Parameters)
                 .WithOne(p => p.Category)
                 .HasForeignKey(p => p.CategoryId)
                 .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Agg.BovinueMetricParameter>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Parameter).HasConversion<string>();

                b.HasIndex(x => new { x.CategoryId, x.Parameter }).IsUnique();
            });

            // =========================
            // Métricas por bovino (Agg)
            // =========================
            modelBuilder.Entity<Agg.BovinueMetric>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();
                b.Property(x => x.Date).IsRequired();
                b.Property(x => x.Quantity).IsRequired();

                b.HasOne(x => x.bovinue)
                 .WithMany()
                 .HasForeignKey(x => x.BovinueId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.parameter)
                 .WithMany()
                 .HasForeignKey(x => x.BovinueMPId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasIndex(x => new { x.BovinueId, x.BovinueMPId, x.Date }).IsUnique();
                b.HasQueryFilter(x => !x.deleted);
            });

            // =========================
            // Plantillas de salud (Agg)
            // =========================
            modelBuilder.Entity<Agg.BovinueCattleHealthRecord>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();

                b.Property(x => x.ActivityName).IsRequired().HasMaxLength(50);
                b.Property(x => x.Description).IsRequired().HasMaxLength(50);
                b.Property(x => x.Frequency).IsRequired(); // mapea a columna "Frecuency" por [Column] en la entidad

                b.HasQueryFilter(x => !x.deleted);
            });

            // =========================
            // Registros de salud (Agg)
            // =========================
            modelBuilder.Entity<Agg.BovinueHealthRecord>(b =>
            {
                b.HasKey(x => x.Id);
                b.Property(x => x.Id).ValueGeneratedOnAdd();

                b.Property(x => x.StartDate).IsRequired();
                b.Property(x => x.EndDate); // puede ser null (abierto)

                b.HasOne(x => x.bovinue)
                 .WithMany()
                 .HasForeignKey(x => x.BovinueId)
                 .OnDelete(DeleteBehavior.Restrict);

                b.HasOne(x => x.bovinueCHR)
                 .WithMany()
                 .HasForeignKey(x => x.BovinueCHRId)
                 .OnDelete(DeleteBehavior.Restrict);

                // Solo un registro ABIERTO por bovino+plantilla
                b.HasIndex(x => new { x.BovinueId, x.BovinueCHRId })
                 .IsUnique()
                 .HasFilter("[EndDate] IS NULL");

                b.HasQueryFilter(x => !x.deleted);
            });
        }
    }
}
