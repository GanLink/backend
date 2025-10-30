using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Interfaces.REST.Resources;
using GanLink.IAM.Interfaces.REST.Transform;

public static class FarmResourceFromEntityAssembler
{
    public static FarmResource ToResourceFromEntity(Farm f)
    {
        var userRes = f.User is not null
            ? UserResourceFromEntityAssembler.ToResourceFromEntity(f.User)
            : null;

        return new FarmResource(
            f.Id,
            f.Alias,
            f.Description,
            f.UserId,
            f.MainActivity.ToString(),
            f.OwnerDni
        );
    }
}