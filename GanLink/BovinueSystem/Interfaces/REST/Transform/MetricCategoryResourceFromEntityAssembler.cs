using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class MetricCategoryResourceFromEntityAssembler
    {
        public static MetricCategoryResource ToResourceFromEntity(BovinueMetricCategory entity)
        {
            return new MetricCategoryResource(
                entity.Id,
                entity.Category
            );
        }
    }
}