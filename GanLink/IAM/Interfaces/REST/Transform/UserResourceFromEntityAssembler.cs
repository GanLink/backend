using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.IAM.Interfaces.REST.Transform;

public static class UserResourceFromEntityAssembler
{
    public static UserResource ToResourceFromEntity(User entity)
        => new UserResource(entity.Username, entity.Firstname, entity.Lastname, entity.Email,
            entity.Ruc,
            entity.Password);
}