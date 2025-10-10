using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Commands;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueMetricCommandService
    {
        Task<BovinueMetric> Handle(CreateBovinueMetricCommand command);
        Task<BovinueMetric> Handle(UpdateBovinueMetricCommand command);
        Task Handle(DeleteBovinueMetricCommand command);
    }
}