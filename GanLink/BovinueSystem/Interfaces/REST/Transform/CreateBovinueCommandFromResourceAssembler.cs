using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class CreateBovinueCommandFromResourceAssembler
    {
        public static CreateBovinueCommand ToCommandFromResource(CreateBovinueResource resource)
        {
            return new CreateBovinueCommand(resource.FarmId);
        }
    }
}