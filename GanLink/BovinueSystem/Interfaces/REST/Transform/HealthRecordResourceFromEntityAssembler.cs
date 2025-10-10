using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class HealthRecordResourceFromEntityAssembler
    {
        public static HealthRecordResource ToResourceFromEntity(BovinueHealthRecord entity)
        {
            return new HealthRecordResource(
                entity.Id,
                entity.BovinueCHRId,
                entity.BovinueId,
                entity.StartDate,
                entity.EndDate,
                entity.BovinueCattleHealthRecord?.ActivityName ?? string.Empty,
                entity.BovinueCattleHealthRecord?.Description ?? string.Empty,
                entity.CreatedDate,
                entity.UpdatedDate
            );
        }
    }
}