using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class BovinueResourceFromEntityAssembler
    {
        public static BovinueResource ToResourceFromEntity(Bovinue entity)
        {
            return new BovinueResource(
                entity.Id,
                entity.FarmId,
                entity.CreatedDate,
                entity.UpdatedDate
            );
        }
    }
}