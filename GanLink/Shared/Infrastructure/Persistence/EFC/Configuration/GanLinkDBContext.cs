using GanLink.FarmManagement.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.IAM.Infraestructure.Persistence.EF.Configuration.Extensions;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration.Extensions;
using Microsoft.EntityFrameworkCore;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class GanLinkDBContext : DbContext
{
    
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
        
        modelBuilder.ApplyFarmingConfiguration();
        
        modelBuilder.UseSnakeCaseNamingConvention();
    }
}