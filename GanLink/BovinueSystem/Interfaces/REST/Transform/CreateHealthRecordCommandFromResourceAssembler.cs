using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class CreateHealthRecordCommandFromResourceAssembler
    {
        public static CreateBovinueHealthRecordCommand ToCommandFromResource(CreateHealthRecordResource resource)
        {
            return new CreateBovinueHealthRecordCommand(
                resource.BovinueCHRId,
                resource.BovinueId,
                resource.StartDate,
                resource.EndDate
            );
        }
    }
}