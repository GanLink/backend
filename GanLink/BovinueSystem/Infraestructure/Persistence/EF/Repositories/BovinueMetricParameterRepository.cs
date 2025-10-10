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
    public class BovinueMetricParameterRepository : BaseRepository<BovinueMetricParameter>, IBovinueMetricParameterRepository
    {
        public BovinueMetricParameterRepository(GanLinkDbContext context) : base(context)
        {
        }

        public async Task<BovinueMetricParameter?> GetByIdAsync(long id)
        {
            return await Context.Set<BovinueMetricParameter>()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<ICollection<BovinueMetricParameter>> GetByCategoryIdAsync(long categoryId)
        {
            return await Context.Set<BovinueMetricParameter>()
                .Where(p => p.CategoryId == categoryId)
                .Include(p => p.Category)
                .OrderBy(p => p.Parameter)
                .ToListAsync();
        }

        public async Task<ICollection<BovinueMetricParameter>> GetAllActiveAsync()
        {
            return await Context.Set<BovinueMetricParameter>()
                .Include(p => p.Category)
                .OrderBy(p => p.CategoryId)
                .ThenBy(p => p.Parameter)
                .ToListAsync();
        }

        public async Task<BovinueMetricParameter?> GetByParameterNameAsync(string parameter)
        {
            return await Context.Set<BovinueMetricParameter>()
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Parameter == parameter);
        }
    }
}