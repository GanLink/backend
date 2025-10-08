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
    public class BovinueCattleHealthRecordRepository : BaseRepository<BovinueCattleHealthRecord>, IBovinueCattleHealthRecordRepository
    {
        public BovinueCattleHealthRecordRepository(GanLinkDbContext context) : base(context)
        {
        }

        public async Task<BovinueCattleHealthRecord?> GetByIdAsync(long id)
        {
            return await Context.Set<BovinueCattleHealthRecord>()
                .FirstOrDefaultAsync(chr => chr.Id == id);
        }

        public async Task<ICollection<BovinueCattleHealthRecord>> GetAllActiveAsync()
        {
            return await Context.Set<BovinueCattleHealthRecord>()
                .OrderBy(chr => chr.ActivityName)
                .ToListAsync();
        }

        public async Task<BovinueCattleHealthRecord?> GetByActivityNameAsync(string activityName)
        {
            return await Context.Set<BovinueCattleHealthRecord>()
                .FirstOrDefaultAsync(chr => chr.ActivityName == activityName);
        }

        public async Task<ICollection<BovinueCattleHealthRecord>> GetByFrequencyAsync(int frequency)
        {
            return await Context.Set<BovinueCattleHealthRecord>()
                .Where(chr => chr.Frequency == frequency)
                .OrderBy(chr => chr.ActivityName)
                .ToListAsync();
        }
    }
}