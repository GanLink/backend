using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Commands;

namespace GanLink.FarmManagement.Domain.Services;

public interface IFarmCommandService
{
    Task<Farm?> Handle(CreateFarmCommand command);

    /// <summary>
    /// Maneja la lógica para eliminar un Farm.
    /// </summary>
    /// <param name="command">El comando que contiene el ID del Farm a eliminar.</param>
    Task Handle(DeleteFarmCommand command);
    
    // --- NUEVO MÉTODO ---
    /// <summary>
    /// Maneja la subida de una imagen para una granja.
    /// </summary>
    /// <param name="command">El comando con el ID y el archivo.</param>
    /// <returns>La URL pública de la imagen guardada.</returns>
    Task<string> Handle(UploadFarmImageCommand command);
}