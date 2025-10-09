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
    public class BovinueMetricCategoryRepository : BaseRepository<BovinueMetricCategory>, IBovinueMetricCategoryRepository
    {
        public BovinueMetricCategoryRepository(GanLinkDbContext context) : base(context)
        {
        }

        public async Task<BovinueMetricCategory?> GetByIdAsync(long id)
        {
            return await Context.Set<BovinueMetricCategory>()
                .Include(c => c.MetricParameters)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ICollection<BovinueMetricCategory>> GetAllActiveAsync()
        {
            return await Context.Set<BovinueMetricCategory>()
                .Include(c => c.MetricParameters)
                .OrderBy(c => c.Category)
                .ToListAsync();
        }

        public async Task<BovinueMetricCategory?> GetByCategoryNameAsync(string category)
        {
            return await Context.Set<BovinueMetricCategory>()
                .Include(c => c.MetricParameters)
                .FirstOrDefaultAsync(c => c.Category == category);
        }
    }
}