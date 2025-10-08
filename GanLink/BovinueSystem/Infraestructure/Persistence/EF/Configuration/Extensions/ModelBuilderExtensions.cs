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
            });
        }
    }
}
