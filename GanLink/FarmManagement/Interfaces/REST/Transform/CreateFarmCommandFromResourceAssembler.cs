using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Interfaces.REST.Resources;

namespace GanLink.FarmManagement.Interfaces.REST.Transform;

public static class CreateFarmCommandFromResourceAssembler
{
    public static CreateFarmCommand ToCommandFromResource(this CreateFarmResource resource)
    {
        return new CreateFarmCommand(
            resource.Alias,
            resource.Description,
            resource.UserId,
            resource.MainActivity,
            resource.OwnerDni
        );
    }
}