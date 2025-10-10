using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueMetricQueryService
    {
        Task<BovinueMetric?> Handle(GetBovinueMetricByIdQuery query);
        Task<IEnumerable<BovinueMetric>> Handle(GetAllBovinueMetricsQuery query);
        Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByBovinueIdQuery query);
        Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByDateRangeQuery query);
        Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByParameterQuery query);
        Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsPagedQuery query);
    }
}