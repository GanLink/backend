using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    // DATASET - Query only service
    public interface IBovinueMetricCategoryQueryService
    {
        Task<BovinueMetricCategory?> Handle(GetBovinueMetricCategoryByIdQuery query);
        Task<IEnumerable<BovinueMetricCategory>> Handle(GetAllBovinueMetricCategoriesQuery query);
    }
}