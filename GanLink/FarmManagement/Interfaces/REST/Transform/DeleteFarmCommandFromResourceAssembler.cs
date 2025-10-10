using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Interfaces.REST.Resources;

namespace GanLink.FarmManagement.Interfaces.REST.Transform;

public static class DeleteFarmCommandFromResourceAssembler
{
    public static DeleteFarmCommand ToCommandFromEntity(DeleteFarmResource resource)
    {
        return new DeleteFarmCommand(resource.userId);
    }
}