using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    // DATASET - Read only repository
    public interface IBovinueMetricCategoryRepository : IBaseRepository<BovinueMetricCategory>
    {
        Task<BovinueMetricCategory?> GetByIdAsync(long id);
        Task<ICollection<BovinueMetricCategory>> GetAllActiveAsync();
        Task<BovinueMetricCategory?> GetByCategoryNameAsync(string category);
    }
}