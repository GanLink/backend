using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

public interface IFarmRepository : IBaseRepository<Farm>
{
    Task<Farm?> GetByIdAsync(int id, CancellationToken ct = default);
    Task<List<Farm>> ListByUserIdAsync(int userId, CancellationToken ct = default);
    Task<Farm?> GetFirstByUserIdAsync(int userId, CancellationToken ct = default); // opcional
    Task<bool> AliasExistsAsync(int userId, string alias, CancellationToken ct = default); // opcional
}