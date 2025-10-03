namespace GanLink.BovinueSystem.Domain.Services;

using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;


public interface IBovinueCommandService
{
    Task<Bovinue?> Handle(CreateBovinueCommand command);
    Task<Bovinue?> Handle(UpdateBovinueCommand command);
    Task<Bovinue?> Handle(DeleteBovinueCommand command);
}

