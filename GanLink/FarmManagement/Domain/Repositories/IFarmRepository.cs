using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.FarmManagement.Domain.Repositories
{
    public interface IFarmRepository : IBaseRepository<Farm>
    {
        // --- Búsqueda por Id (inclúyelo solo si NO está en IBaseRepository) ---
        Task<Farm?> GetByIdAsync(int id, CancellationToken ct = default);

        // --- Lecturas múltiples ---
        Task<IReadOnlyList<Farm>> ListByUserIdAsync(int userId, CancellationToken ct = default);
        Task<IReadOnlyList<Farm>> ListByUserIdAsync(int userId, int page, int pageSize, CancellationToken ct = default);
        Task<int> CountByUserIdAsync(int userId, CancellationToken ct = default);

        // --- Lecturas singulares ---
        Task<Farm?> FindFirstByUserIdAsync(int userId, CancellationToken ct = default);
        Task<Farm?> FindByAliasAsync(int userId, string alias, CancellationToken ct = default);

        // --- Validaciones ---
        Task<bool> AliasExistsAsync(int userId, string alias, CancellationToken ct = default);
    }
}