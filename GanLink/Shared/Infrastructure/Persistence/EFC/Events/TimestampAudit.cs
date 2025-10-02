using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using EntityFrameworkCore.CreatedUpdatedDate.Contracts; 

public class TimestampAudit : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;

        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var now = DateTimeOffset.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries<IEntityWithCreatedUpdatedDate>())
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = now;
                entry.Entity.UpdatedDate = now;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = now;
            }
        }

        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}