namespace GanLink.BovinueSystem.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;


public interface IBovinueMetricCategoryRepository : IBaseRepository<BovinueMetricCategory>
{
    Task<BovinueMetricCategory?> GetByIdAsync(long id);
    Task<ICollection<BovinueMetricCategory>> GetAllAsync();
}

