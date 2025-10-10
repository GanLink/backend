using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class UpdateMetricCommandFromResourceAssembler
    {
        public static UpdateBovinueMetricCommand ToCommandFromResource(long id, UpdateMetricResource resource)
        {
            return new UpdateBovinueMetricCommand(
                id,
                resource.BovinueMPId,
                resource.BovinueId,
                resource.Date,
                resource.Quantity
            );
        }
    }
}