using System;
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
    public class BovinueHealthRecordRepository : BaseRepository<BovinueHealthRecord>, IBovinueHealthRecordRepository
    {
        public BovinueHealthRecordRepository(GanLinkDbContext context) : base(context)
        {
        }

        public async Task<BovinueHealthRecord?> GetByIdAsync(long id)
        {
            return await Context.Set<BovinueHealthRecord>()
                .Include(hr => hr.Bovinue)
                .Include(hr => hr.BovinueCattleHealthRecord)
                .FirstOrDefaultAsync(hr => hr.Id == id);
        }

        public async Task<ICollection<BovinueHealthRecord>> GetByBovinueIdAsync(long bovinueId)
        {
            return await Context.Set<BovinueHealthRecord>()
                .Where(hr => hr.BovinueId == bovinueId)
                .Include(hr => hr.BovinueCattleHealthRecord)
                .OrderByDescending(hr => hr.StartDate)
                .ToListAsync();
        }

        public async Task<ICollection<BovinueHealthRecord>> GetOpenRecordsByBovinueIdAsync(long bovinueId)
        {
            return await Context.Set<BovinueHealthRecord>()
                .Where(hr => hr.BovinueId == bovinueId && 
                            !hr.deleted && 
                            hr.EndDate == null)
                .Include(hr => hr.BovinueCattleHealthRecord)
                .OrderByDescending(hr => hr.StartDate)
                .ToListAsync();
        }

        public async Task<ICollection<BovinueHealthRecord>> GetByDateRangeAsync(long bovinueId, DateTime startDate, DateTime endDate)
        {
            return await Context.Set<BovinueHealthRecord>()
                .Where(hr => hr.BovinueId == bovinueId &&
                            hr.StartDate >= startDate &&
                            hr.StartDate <= endDate)
                .Include(hr => hr.BovinueCattleHealthRecord)
                .OrderByDescending(hr => hr.StartDate)
                .ToListAsync();
        }

        public async Task<bool> HasActiveVaccinationAsync(long bovinueId, long cattleHealthRecordId)
        {
            return await Context.Set<BovinueHealthRecord>()
                .AnyAsync(hr => hr.BovinueId == bovinueId &&
                               hr.BovinueCHRId == cattleHealthRecordId &&
                               !hr.deleted &&
                               hr.EndDate == null);
        }
    }
}
