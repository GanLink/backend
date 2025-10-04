using GanLink.IAM.Domain.Models.Commands;
using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.IAM.Interfaces.REST.Transform;

public static class SignUpCommandFromResourceAssembler
{
    public static SignUpCommand ToCommandFromResource(SignUpResource resource)
    {
        return new SignUpCommand(resource.Username,
            resource.Email,
            resource.Password, 
            resource.TypeUser, 
            resource.MaxDailyReservationHour,
            resource.IdentificationUser
        );
    }
}