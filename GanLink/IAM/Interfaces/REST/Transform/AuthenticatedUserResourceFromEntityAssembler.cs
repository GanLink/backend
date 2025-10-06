using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.IAM.Interfaces.REST.Transform;

public static class AuthenticatedUserResourceFromEntityAssembler
{
    public static AuthenticatedUserResource ToResourceFromEntity(User user,string token)
    {
        return new AuthenticatedUserResource(user.Id, user.Username, token);
    }
}