namespace GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;

using GanLink.Shared.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;

public class UnitOfWork(GanLinkDBContext context) : IUnitOfWork
{
    public async Task CompleteAsync()
    {
        await context.SaveChangesAsync();
    }
}