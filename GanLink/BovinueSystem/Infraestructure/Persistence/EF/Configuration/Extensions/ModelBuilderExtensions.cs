using GanLink.BovinueSystem.Domain.Models.Aggregates;
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
        modelBuilder.Entity<BovinueMetricCategory>().Property(x => x.Category);
        
        modelBuilder.Entity<BovinueMetricParameter>().HasKey(x => x.Id);
        modelBuilder.Entity<BovinueMetricParameter>().Property(x => x.Id).ValueGeneratedOnAdd();
        modelBuilder.Entity<BovinueMetricParameter>().HasOne(x => x.Category)
            .WithMany(o =>o.Parameters)
            .HasForeignKey(x => x.CategoryId);
        
        

    }
}