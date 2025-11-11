using GanLink.BovinueSystem.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyBovinueSystemConfiguration(this ModelBuilder modelBuilder)
        {
            // =========================
            // Bovinue
            // =========================
            modelBuilder.Entity<Bovinue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FarmId).IsRequired();
                entity.Property(e => e.deleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(e => e.farm)
                    .WithMany()
                    .HasForeignKey(e => e.FarmId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .HasConstraintName("fk_bov_farm");

                entity.HasIndex(e => e.FarmId).HasDatabaseName("ix_bov_farm_id");
            });

            // =========================
            // BovinueHealthRecord
            // =========================
            modelBuilder.Entity<BovinueHealthRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BovinueCHRId).IsRequired();
                entity.Property(e => e.BovinueId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.deleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(e => e.Bovinue)
                    .WithMany(b => b.HealthRecords)
                    .HasForeignKey(e => e.BovinueId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_bhr_bovinue");

                entity.HasIndex(e => e.BovinueId).HasDatabaseName("ix_bhr_bovinue_id");

                entity.HasOne(e => e.BovinueCattleHealthRecord)
                    .WithMany(chr => chr.BovinueHealthRecords)
                    .HasForeignKey(e => e.BovinueCHRId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_bhr_chr");

                entity.HasIndex(e => e.BovinueCHRId).HasDatabaseName("ix_bhr_chr_id");
            });

            // =========================
            // BovinueMetric
            // =========================
            modelBuilder.Entity<BovinueMetric>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BovinueMPId).IsRequired();
                entity.Property(e => e.BovinueId).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.deleted).IsRequired().HasDefaultValue(false);

                entity.HasOne(e => e.Bovinue)
                    .WithMany(b => b.Metrics)
                    .HasForeignKey(e => e.BovinueId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_bm_bovinue");

                entity.HasIndex(e => e.BovinueId).HasDatabaseName("ix_bm_bovinue_id");

                entity.HasOne(e => e.BovinueMetricParameter)
                    .WithMany(mp => mp.Metrics)
                    .HasForeignKey(e => e.BovinueMPId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_bm_param");

                entity.HasIndex(e => e.BovinueMPId).HasDatabaseName("ix_bm_param_id");
            });

            // =========================
            // BovinueCattleHealthRecord (Dataset)
            //  - IDs fijos para seeders
            //  - Longitudes consistentes
            //  - Índice único para idempotencia
            // =========================
            modelBuilder.Entity<BovinueCattleHealthRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); // IDs del seeder
                entity.Property(e => e.ActivityName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Frequency).IsRequired();
                entity.Property(e => e.Description).IsRequired().HasMaxLength(500);

                entity.HasIndex(e => e.ActivityName)
                      .IsUnique()
                      .HasDatabaseName("ux_bchr_activity");
                entity.HasIndex(e => e.ActivityName).HasDatabaseName("ix_bm_activity_name");
                entity.HasData(
                    new
                    {
                        Id = 1L,
                        ActivityName = "Identificación Oficial (Aretado)",
                        Frequency = 0, // una sola vez en la vida
                        Description = "El arete con código único es la identidad del animal y base de trazabilidad.",
                        deleted = false
                    },
                    new
                    {
                        Id = 2L,
                        ActivityName = "Vacunación contra la Fiebre Aftosa",
                        Frequency = -1, // periódica (según campañas SENASA)
                        Description = "Previene enfermedad altamente contagiosa y asegura control sanitario nacional.",
                        deleted = false
                    },
                    new
                    {
                        Id = 3L,
                        ActivityName = "Vacunación contra la Brucelosis Bovina",
                        Frequency = 0, // una sola vez en la vida (hembras 3–8 meses)
                        Description = "Protege contra enfermedad reproductiva, inocuidad de leche y sanidad genética.",
                        deleted = false
                    }
                );
            });

            // =========================
            // BovinueMetricCategory (Dataset)
            //  - IDs fijos
            //  - Unique por Category
            // =========================
            modelBuilder.Entity<BovinueMetricCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); // IDs del seeder
                entity.Property(e => e.Category).IsRequired().HasMaxLength(100);

                entity.HasIndex(e => e.Category)
                      .IsUnique()
                      .HasDatabaseName("ux_bmc_category");
                entity.HasData(
                    new { Id = 1L, Category = "Productivos (Leche)",    deleted = false },
                    new { Id = 2L, Category = "Productivos (Carne)",    deleted = false },
                    new { Id = 3L, Category = "Eficiencia Alimenticia", deleted = false },
                    new { Id = 4L, Category = "Reproductivos / Preñez", deleted = false },
                    new { Id = 5L, Category = "Genéticos",              deleted = false }
                );
            });

            // =========================
            // BovinueMetricParameter (Dataset)
            //  - IDs fijos
            //  - Unique por (CategoryId, Parameter)
            //  - FK con nombre corto
            // =========================
            modelBuilder.Entity<BovinueMetricParameter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedNever(); // IDs del seeder
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.Parameter).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);

                entity.HasOne(e => e.Category)
                    .WithMany(c => c.MetricParameters)
                    .HasForeignKey(e => e.CategoryId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("fk_bmp_category");

                // Índices
                entity.HasIndex(e => e.CategoryId).HasDatabaseName("ix_bmp_category_id");

                entity.HasIndex(e => new { e.CategoryId, e.Parameter })
                      .IsUnique()
                      .HasDatabaseName("ux_bmp_cat_param");
                entity.HasData(
                    // Productivos (Leche)
                    new { Id = 1L, CategoryId = 1L, Parameter = "Producción de leche por vaca/día", Description = "Nivel de rendimiento individual", deleted = false },
                    new { Id = 2L, CategoryId = 1L, Parameter = "Contenido de grasa y proteína",    Description = "Calidad nutricional de la leche", deleted = false },

                    // Productivos (Carne)
                    new { Id = 3L, CategoryId = 2L, Parameter = "Ganancia de peso diaria (GMD)",    Description = "Crecimiento y eficiencia de engorde", deleted = false },

                    // Eficiencia Alimenticia
                    new { Id = 4L, CategoryId = 3L, Parameter = "Índice de conversión alimenticia", Description = "Eficiencia: kg de alimento necesarios para ganar 1 kg de peso", deleted = false },

                    // Reproductivos / Preñez
                    new { Id = 5L, CategoryId = 4L, Parameter = "Tasa de preñez",                   Description = "Porcentaje de vacas que quedan gestantes", deleted = false },
                    new { Id = 6L, CategoryId = 4L, Parameter = "Tasa de concepción",               Description = "Éxito de preñez respecto a servicios realizados", deleted = false },

                    // Genéticos
                    new { Id = 7L, CategoryId = 5L, Parameter = "Árbol genealógico / pedigree",     Description = "Registro de linaje y control de consanguinidad", deleted = false }
                );
            });
        }
    }
}
