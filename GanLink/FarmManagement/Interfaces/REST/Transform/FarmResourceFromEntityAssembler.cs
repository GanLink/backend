using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Interfaces.REST.Resources;
using GanLink.IAM.Interfaces.REST.Transform;

namespace GanLink.FarmManagement.Interfaces.REST.Transform;

public static class FarmResourceFromEntityAssembler
{
    public static FarmResource ToResourceFromEntity(Farm farm)
    {
        return new FarmResource(
            farm.Id,
            farm.user != null
                ? UserResourceFromEntityAssembler.ToResourceFromEntity(farm.user)
                : null
        );
    }
    
}