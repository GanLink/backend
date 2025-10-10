using GanLink.IAM.Interfaces.REST.Resources;

namespace GanLink.FarmManagement.Interfaces.REST.Resources;

public record FarmResource(
    int Id,
    string Alias,
    int UserId,
    string MainActivity, // "CARNE" | "LECHE" | "GENERICA"
    string OwnerDni
);