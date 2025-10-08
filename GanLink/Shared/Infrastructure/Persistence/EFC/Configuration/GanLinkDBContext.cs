using GanLink.IAM.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using GanLink.BovinueSystem.Infrastructure.Persistence.EF.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class GanLinkDBContext : DbContext
{
    // DbSets para Bovine System
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.Bovinue> Bovinues { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueHealthRecord> BovinueHealthRecords { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetric> BovinueMetrics { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueCattleHealthRecord> BovinueCattleHealthRecords { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetricCategory> BovinueMetricCategories { get; set; }
    public DbSet<GanLink.BovinueSystem.Domain.Models.Aggregates.BovinueMetricParameter> BovinueMetricParameters { get; set; }
    
    // DbSets para IAM (si no los tienes ya)
    public DbSet<GanLink.IAM.Domain.Models.Aggregates.User> Users { get; set; }
    
    // DbSets para Farm Management (si no los tienes ya)
    public DbSet<GanLink.FarmManagement.Domain.Models.Aggregates.Farm> Farms { get; set; }
    
    public GanLinkDBContext(DbContextOptions<GanLinkDBContext> options) : base(options){}
    
    private readonly TimestampAudit _timestampsAudit;
    
    public GanLinkDBContext(DbContextOptions<GanLinkDBContext> options, TimestampAudit timestampsAudit)
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
        
        // Aplicar configuración del Bovine System
        modelBuilder.ApplyBovinueSystemConfiguration();
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}