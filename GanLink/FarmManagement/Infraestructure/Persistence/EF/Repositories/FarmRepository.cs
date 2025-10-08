using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GanLink.FarmManagement.Infrastructure.Persistence.EFC.Repositories
{
    public class FarmRepository : BaseRepository<Farm>, IFarmRepository
    {
        public FarmRepository(GanLinkDBContext context) : base(context) { }

        private DbSet<Farm> Farms => Context.Set<Farm>();

        // ---------------------------
        // Búsqueda por Id
        // ---------------------------
        public async Task<Farm?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await Farms
                .FirstOrDefaultAsync(f => f.Id == id, ct)
                .ConfigureAwait(false);
        }

        // ---------------------------
        // Lecturas múltiples
        // ---------------------------
        public async Task<IReadOnlyList<Farm>> ListByUserIdAsync(int userId, CancellationToken ct = default)
        {
            var list = await Farms
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Alias)
                .ToListAsync(ct)
                .ConfigureAwait(false);

            return list; // upcast implícito a IReadOnlyList<Farm>
        }

        public async Task<IReadOnlyList<Farm>> ListByUserIdAsync(int userId, int page, int pageSize, CancellationToken ct = default)
        {
            if (page < 1) page = 1;
            if (pageSize <= 0) pageSize = 10;

            var list = await Farms
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Alias)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct)
                .ConfigureAwait(false);

            return list;
        }

        public async Task<int> CountByUserIdAsync(int userId, CancellationToken ct = default)
        {
            return await Farms
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .CountAsync(ct)
                .ConfigureAwait(false);
        }

        // ---------------------------
        // Lecturas singulares
        // ---------------------------
        public async Task<Farm?> FindFirstByUserIdAsync(int userId, CancellationToken ct = default)
        {
            return await Farms
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Id)
                .FirstOrDefaultAsync(ct)
                .ConfigureAwait(false);
        }

        public async Task<Farm?> FindByAliasAsync(int userId, string alias, CancellationToken ct = default)
        {
            var norm = (alias ?? string.Empty).Trim();
            if (norm.Length == 0) return null;

            return await Farms
                .AsNoTracking()
                .FirstOrDefaultAsync(f => f.UserId == userId && f.Alias == norm, ct)
                .ConfigureAwait(false);
        }

        // ---------------------------
        // Validaciones
        // ---------------------------
        public async Task<bool> AliasExistsAsync(int userId, string alias, CancellationToken ct = default)
        {
            var norm = (alias ?? string.Empty).Trim();
            if (norm.Length == 0) return false;

            return await Farms
                .AsNoTracking()
                .AnyAsync(f => f.UserId == userId && f.Alias == norm, ct)
                .ConfigureAwait(false);
        }
    }
}
