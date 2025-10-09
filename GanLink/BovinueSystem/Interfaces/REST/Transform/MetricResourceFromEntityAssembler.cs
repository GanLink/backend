using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class MetricResourceFromEntityAssembler
    {
        public static MetricResource ToResourceFromEntity(BovinueMetric entity)
        {
            return new MetricResource(
                entity.Id,
                entity.BovinueMPId,
                entity.BovinueId,
                entity.Date,
                entity.Quantity,
                entity.BovinueMetricParameter?.Parameter ?? string.Empty,
                entity.BovinueMetricParameter?.Category?.Category ?? string.Empty,
                entity.CreatedDate,
                entity.UpdatedDate
            );
        }
    }
}