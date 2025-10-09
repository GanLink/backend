using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class UpdateBovinueCommandFromResourceAssembler
    {
        public static UpdateBovinueCommand ToCommandFromResource(long id, UpdateBovinueResource resource)
        {
            return new UpdateBovinueCommand(id, resource.FarmId);
        }
    }
}