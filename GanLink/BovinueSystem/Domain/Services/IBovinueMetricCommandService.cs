namespace GanLink.BovinueSystem.Domain.Services;

using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;

public interface IBovinueMetricCommandService
{
    Task<BovinueMetric?> Handle(CreateBovinueMetricCommand command);
    Task<BovinueMetric?> Handle(UpdateBovinueMetricCommand command);
    Task<BovinueMetric?> Handle(DeleteBovinueMetricCommand command);
}
