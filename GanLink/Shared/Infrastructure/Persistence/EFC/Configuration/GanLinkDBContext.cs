using GanLink.BovinueSystem.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.FarmManagement.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.IAM.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class GanLinkDbContext : DbContext
{
    // DbSets para Bovine System
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.Bovinue> Bovinues { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueHealthRecord> BovinueHealthRecords { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetric> BovinueMetrics { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueCattleHealthRecord> BovinueCattleHealthRecords { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetricCategory> BovinueMetricCategories { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetricParameter> BovinueMetricParameters { get; set; }
    
   
    public GanLinkDbContext(DbContextOptions<GanLinkDbContext> options) : base(options){}
    
    private readonly TimestampAudit _timestampsAudit;
    
    public GanLinkDbContext(DbContextOptions<GanLinkDbContext> options, TimestampAudit timestampsAudit)
        : base(options)
    {
        _timestampsAudit = timestampsAudit;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.AddInterceptors(_timestampsAudit);
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // add builder entities 
        
        modelBuilder.ApplyIamConfiguration();
        
        modelBuilder.ApplyFarmingConfiguration();
        
        // Aplicar configuración del Bovine System
        modelBuilder.ApplyBovinueSystemConfiguration();
            
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}