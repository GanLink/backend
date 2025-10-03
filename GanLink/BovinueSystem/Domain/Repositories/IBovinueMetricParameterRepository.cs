namespace GanLink.BovinueSystem.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;


public interface IBovinueMetricParameterRepository : IBaseRepository<BovinueMetricParameter>
{
    Task<BovinueMetricParameter?> GetByIdAsync(long id);
    Task<ICollection<BovinueMetricParameter>> GetByCategoryIdAsync(long categoryId);
    Task<BovinueMetricParameter?> FindByNameInCategoryAsync(long categoryId, string parameter);
}

