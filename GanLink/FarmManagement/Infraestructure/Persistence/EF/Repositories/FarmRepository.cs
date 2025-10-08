using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GanLink.FarmManagement.Infraestructure.Persistence.EF.Repositories
{
    public class FarmRepository : BaseRepository<Farm>, IFarmRepository
    {
        public FarmRepository(GanLinkDbContext context) : base(context) { }

        // Suele usarse para modificar luego: devuélvelo con tracking
        public async Task<Farm?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await Context.Set<Farm>()
                .FirstOrDefaultAsync(f => f.Id == id, ct);
        }

        // Cardinalidad correcta: un usuario puede tener varias farms
        public async Task<List<Farm>> ListByUserIdAsync(int userId, CancellationToken ct = default)
        {
            return await Context.Set<Farm>()
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Alias)        // orden estable para UI
                .ToListAsync(ct);
        }

        // Si quieres mantener uno que devuelva una sola (por compatibilidad):
        // Deja explícito qué devuelve: la "primera" del usuario (convención)
        public async Task<Farm?> GetFirstByUserIdAsync(int userId, CancellationToken ct = default)
        {
            return await Context.Set<Farm>()
                .AsNoTracking()
                .Where(f => f.UserId == userId)
                .OrderBy(f => f.Id)
                .FirstOrDefaultAsync(ct);
        }

        // Helper útil de dominio: validar unicidad de alias por usuario
        public async Task<bool> AliasExistsAsync(int userId, string alias, CancellationToken ct = default)
        {
            return await Context.Set<Farm>()
                .AnyAsync(f => f.UserId == userId && f.Alias == alias, ct);
        }
    }
}
