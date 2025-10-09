using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueHealthRecordCommandService
    {
        Task<BovinueHealthRecord> Handle(CreateBovinueHealthRecordCommand command);
        Task<BovinueHealthRecord> Handle(UpdateBovinueHealthRecordCommand command);
        Task Handle(DeleteBovinueHealthRecordCommand command);
    }
}