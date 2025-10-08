using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    // DATASET - Query only service
    public interface IBovinueMetricParameterQueryService
    {
        Task<BovinueMetricParameter?> Handle(GetBovinueMetricParameterByIdQuery query);
        Task<IEnumerable<BovinueMetricParameter>> Handle(GetAllBovinueMetricParametersQuery query);
        Task<IEnumerable<BovinueMetricParameter>> Handle(GetBovinueMetricParametersByCategoryIdQuery query);
    }
}