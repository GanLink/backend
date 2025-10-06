namespace GanLink.BovinueSystem.Domain.Services;

using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;


public interface IBovinueHealthRecordCommandService
{
    Task<BovinueHealthRecord?> Handle(CreateBovinueHealthRecordCommand command);
    Task<BovinueHealthRecord?> Handle(UpdateBovinueHealthRecordCommand command);
    Task<BovinueHealthRecord?> Handle(DeleteBovinueHealthRecordCommand command);
}

