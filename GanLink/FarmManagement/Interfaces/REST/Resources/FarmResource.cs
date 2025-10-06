using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.FarmManagement.Interfaces.REST.Resources;

public record FarmResource(
    int Id,
    //AliasResource? Alias,
    UserResource? UserId
    //ActivityResource? MainActivity,
    //OwnerDniResource? OwnerDni,
    
    
    );