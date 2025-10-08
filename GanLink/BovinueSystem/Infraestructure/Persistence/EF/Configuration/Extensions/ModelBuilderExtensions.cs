using GanLink.BovinueSystem.Domain.Models.Aggregates;
using Microsoft.EntityFrameworkCore;

namespace GanLink.BovinueSystem.Infrastructure.Persistence.EF.Configuration.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void ApplyBovinueSystemConfiguration(this ModelBuilder modelBuilder)
        {
            // Bovinue configuration
            modelBuilder.Entity<Bovinue>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.FarmId).IsRequired();
                entity.Property(e => e.deleted).IsRequired().HasDefaultValue(false);
                
                entity.HasOne(e => e.farm)
                    .WithMany()
                    .HasForeignKey(e => e.FarmId)
                    .IsRequired();
            });

            // BovinueHealthRecord configuration
            modelBuilder.Entity<BovinueHealthRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.BovinueCHRId).IsRequired();
                entity.Property(e => e.BovinueId).IsRequired();
                entity.Property(e => e.StartDate).IsRequired();
                entity.Property(e => e.deleted).IsRequired().HasDefaultValue(false);
                
                entity.HasOne(e => e.Bovinue)
                    .WithMany(b => b.HealthRecords)
                    .HasForeignKey(e => e.BovinueId);
                
                entity.HasOne(e => e.BovinueCattleHealthRecord)
                    .WithMany(chr => chr.BovinueHealthRecords)
                    .HasForeignKey(e => e.BovinueCHRId);
            });

            // BovinueMetric configuration
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
                    .HasForeignKey(e => e.BovinueId);
                
                entity.HasOne(e => e.BovinueMetricParameter)
                    .WithMany(mp => mp.Metrics)
                    .HasForeignKey(e => e.BovinueMPId);
            });

            // BovinueCattleHealthRecord configuration (Dataset)
            modelBuilder.Entity<BovinueCattleHealthRecord>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.ActivityName).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Frequency).IsRequired();
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // BovinueMetricCategory configuration (Dataset)
            modelBuilder.Entity<BovinueMetricCategory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(100);
            });

            // BovinueMetricParameter configuration (Dataset)
            modelBuilder.Entity<BovinueMetricParameter>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CategoryId).IsRequired();
                entity.Property(e => e.Parameter).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                
                entity.HasOne(e => e.Category)
                    .WithMany(c => c.MetricParameters)
                    .HasForeignKey(e => e.CategoryId);
            });
        }
    }
}