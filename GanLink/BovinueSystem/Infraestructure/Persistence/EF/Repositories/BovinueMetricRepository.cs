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
    public class BovinueMetricRepository : BaseRepository<BovinueMetric>, IBovinueMetricRepository
    {
        public BovinueMetricRepository(GanLinkDBContext context) : base(context)
        {
        }

        public async Task<BovinueMetric?> GetByIdAsync(long id)
        {
            return await Context.Set<BovinueMetric>()
                .Include(m => m.Bovinue)
                .Include(m => m.BovinueMetricParameter)
                    .ThenInclude(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<ICollection<BovinueMetric>> GetByBovinueIdAsync(long bovinueId)
        {
            return await Context.Set<BovinueMetric>()
                .Where(m => m.BovinueId == bovinueId)
                .Include(m => m.BovinueMetricParameter)
                    .ThenInclude(p => p.Category)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<ICollection<BovinueMetric>> GetByParameterIdAsync(long bovinueId, long parameterId)
        {
            return await Context.Set<BovinueMetric>()
                .Where(m => m.BovinueId == bovinueId && 
                           m.BovinueMPId == parameterId)
                .Include(m => m.BovinueMetricParameter)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<ICollection<BovinueMetric>> GetByDateRangeAsync(long bovinueId, DateTime startDate, DateTime endDate)
        {
            return await Context.Set<BovinueMetric>()
                .Where(m => m.BovinueId == bovinueId &&
                           m.Date >= startDate.Date &&
                           m.Date <= endDate.Date)
                .Include(m => m.BovinueMetricParameter)
                    .ThenInclude(p => p.Category)
                .OrderByDescending(m => m.Date)
                .ToListAsync();
        }

        public async Task<bool> ExistsForDateAsync(long bovinueId, long parameterId, DateTime date)
        {
            return await Context.Set<BovinueMetric>()
                .AnyAsync(m => m.BovinueId == bovinueId &&
                              m.BovinueMPId == parameterId &&
                              m.Date.Date == date.Date &&
                              !m.deleted);
        }

        public async Task<BovinueMetric?> GetLatestByParameterAsync(long bovinueId, long parameterId)
        {
            return await Context.Set<BovinueMetric>()
                .Where(m => m.BovinueId == bovinueId && 
                           m.BovinueMPId == parameterId &&
                           !m.deleted)
                .Include(m => m.BovinueMetricParameter)
                .OrderByDescending(m => m.Date)
                .FirstOrDefaultAsync();
        }
    }
}