using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class UpdateHealthRecordCommandFromResourceAssembler
    {
        public static UpdateBovinueHealthRecordCommand ToCommandFromResource(long id, UpdateHealthRecordResource resource)
        {
            return new UpdateBovinueHealthRecordCommand(
                id,
                resource.BovinueCHRId,
                resource.BovinueId,
                resource.StartDate,
                resource.EndDate
            );
        }
    }
}