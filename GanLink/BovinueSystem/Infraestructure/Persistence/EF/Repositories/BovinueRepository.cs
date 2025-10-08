using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GanLink.BovinueSystem.Infrastructure.Persistence.EF.Repositories
{
    public class BovinueRepository : BaseRepository<Bovinue>, IBovinueRepository
    {
        public BovinueRepository(GanLinkDbContext context) : base(context)
        {
        }

        public async Task<Bovinue?> GetByIdAsync(long id)
        {
            return await Context.Set<Bovinue>()
                .Include(b => b.HealthRecords)
                .Include(b => b.Metrics)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<ICollection<Bovinue>> GetByFarmIdAsync(long farmId)
        {
            return await Context.Set<Bovinue>()
                .Where(b => b.FarmId == farmId)
                .Include(b => b.HealthRecords)
                .Include(b => b.Metrics)
                .ToListAsync();
        }

        public async Task<bool> HasOpenHealthRecordsAsync(long bovinueId)
        {
            return await Context.Set<BovinueHealthRecord>()
                .AnyAsync(hr => hr.BovinueId == bovinueId && 
                                !hr.deleted && 
                                hr.EndDate == null);
        }
    }
}