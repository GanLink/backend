namespace GanLink.FarmManagement.Domain.Models.Commands;

/// <summary>
/// Comando para subir y asignar una imagen a una granja.
/// </summary>
/// <param name="FarmId">El ID de la granja a modificar.</param>
/// <param name="ImageFile">El archivo de imagen enviado.</param>
public record UploadFarmImageCommand(int FarmId, IFormFile ImageFile);