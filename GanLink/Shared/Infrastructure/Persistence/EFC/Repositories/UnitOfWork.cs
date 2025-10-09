using System.Threading.Tasks;

namespace GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;

using GanLink.Shared.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class UnitOfWork(GanLinkDbContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}