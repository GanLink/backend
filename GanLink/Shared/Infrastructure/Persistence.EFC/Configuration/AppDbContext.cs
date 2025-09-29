using Microsoft.EntityFrameworkCore;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class AppDbContext(DbContextOptions options) : DbContext(options)
{
    
}