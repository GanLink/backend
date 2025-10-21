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
}