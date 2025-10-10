using GanLink.IAM.Domain.Models.Commands;
using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.IAM.Interfaces.REST.Transform;

public static  class SignInCommandFromResourceAssembler
{
    public static SignInCommand ToCommandFromResource(SignInResource signInResource)
    {
        return new SignInCommand(signInResource.Username, signInResource.Password);
    }
}