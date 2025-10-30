using GanLink.FarmManagement.Domain.Models.ValueObjects;

namespace GanLink.FarmManagement.Interfaces.REST.Resources;

public record CreateFarmResource(string Alias, string Description, int UserId, Activity MainActivity, string OwnerDni);