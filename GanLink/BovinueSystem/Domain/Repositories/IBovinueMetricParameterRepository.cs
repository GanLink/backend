using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    // DATASET - Read only repository
    public interface IBovinueMetricParameterRepository : IBaseRepository<BovinueMetricParameter>
    {
        Task<BovinueMetricParameter?> GetByIdAsync(long id);
        Task<ICollection<BovinueMetricParameter>> GetByCategoryIdAsync(long categoryId);
        Task<ICollection<BovinueMetricParameter>> GetAllActiveAsync();
        Task<BovinueMetricParameter?> GetByParameterNameAsync(string parameter);
    }
}