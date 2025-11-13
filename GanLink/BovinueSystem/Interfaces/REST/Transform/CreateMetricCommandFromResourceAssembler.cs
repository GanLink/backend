using GanLink.BovinueSystem.Domain.Models.Commands;
using GanLink.BovinueSystem.Interfaces.REST.Resources;

namespace GanLink.BovinueSystem.Interfaces.REST.Transform
{
    public static class CreateMetricCommandFromResourceAssembler
    {
        public static CreateBovinueMetricCommand ToCommandFromResource(CreateMetricResource resource)
        {
            return new CreateBovinueMetricCommand(
                resource.BovinueId,
                resource.BovinueMPId,
                resource.Date,
                resource.Quantity
            );
        }
    }
}