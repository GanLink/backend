using GanLink.FarmManagement.Domain.Models.ValueObjects;

namespace GanLink.FarmManagement.Domain.Models.Commands;

public record CreateFarmCommand(string Alias, string Description, int UserId, Activity MainActivity, string OwnerDni);