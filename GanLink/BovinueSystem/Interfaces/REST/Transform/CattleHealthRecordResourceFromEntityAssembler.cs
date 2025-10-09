using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class CattleHealthRecordResourceFromEntityAssembler
    {
        public static CattleHealthRecordResource ToResourceFromEntity(BovinueCattleHealthRecord entity)
        {
            return new CattleHealthRecordResource(
                entity.Id,
                entity.ActivityName,
                entity.Frequency,
                entity.Description
            );
        }
    }
}