using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueCommandService
    {
        Task<Bovinue> Handle(CreateBovinueCommand command);
        Task<Bovinue> Handle(UpdateBovinueCommand command);
        Task Handle(DeleteBovinueCommand command);
    }
}