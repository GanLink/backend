namespace GanLink.FarmManagement.Domain.Models.Commands;

/// <summary>
/// Representa el comando para eliminar un Farm.
/// </summary>
/// <param name="FarmId">El ID del Farm que se desea eliminar.</param>
public record DeleteFarmCommand(int FarmId);