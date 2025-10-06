using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Commands;

namespace GanLink.FarmManagement.Domain.Services;

public interface IFarmCommandService
{
    Task<Farm?> Handle(CreateFarmCommand command);

    Task Handle(DeleteFarmCommand command);
}