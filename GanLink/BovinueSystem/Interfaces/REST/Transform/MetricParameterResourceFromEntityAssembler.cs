using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class MetricParameterResourceFromEntityAssembler
    {
        public static MetricParameterResource ToResourceFromEntity(BovinueMetricParameter entity)
        {
            return new MetricParameterResource(
                entity.Id,
                entity.CategoryId,
                entity.Parameter,
                entity.Description,
                entity.Category?.Category ?? string.Empty
            );
        }
    }
}