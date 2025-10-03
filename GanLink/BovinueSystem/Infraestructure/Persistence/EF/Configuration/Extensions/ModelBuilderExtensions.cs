using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace GanLink.BovinueSystem.Infraestructure.Persistence.EF.Configuration.Extensions;

public static class ModelBuilderExtensions
{
    public static void ApplyBovinueSystemConfiguration(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Bovinue>().HasKey(x => x.Id);
        modelBuilder.Entity<Bovinue>().Property(x => x.Id).ValueGeneratedOnAdd();
        
        modelBuilder.Entity<BovinueMetricCategory>().HasKey(x => x.Id);
        modelBuilder.Entity<BovinueMetricCategory>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<BovinueMetricCategory>().Property(x => x.Category)
            .HasConversion<string>();
        
        modelBuilder.Entity<BovinueMetricParameter>().HasKey(x => x.Id);
        modelBuilder.Entity<BovinueMetricParameter>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<BovinueMetricParameter>().Property(x => x.Parameter)
            .HasConversion<string>();
        modelBuilder.Entity<BovinueMetricParameter>().HasOne(x => x.Category)
            .WithMany(o =>o.Parameters)
            .HasForeignKey(x => x.CategoryId);
        
        modelBuilder.Entity<BovinueMetric>().HasKey(x => x.Id);
        modelBuilder.Entity<BovinueMetric>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<BovinueMetric>().Property(x => x.Date);
        modelBuilder.Entity<BovinueMetric>().Property(x => x.Quantity);
        modelBuilder.Entity<BovinueMetric>().HasOne(x => x.bovinue)
            .WithMany()
            .HasForeignKey(x => x.BovinueId);
        modelBuilder.Entity<BovinueMetric>().HasOne(x => x.parameter)
            .WithMany()
            .HasForeignKey(x => x.BovinueMPId);



    }
}