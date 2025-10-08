using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    public interface IBovinueHealthRecordQueryService
    {
        Task<BovinueHealthRecord?> Handle(GetBovinueHealthRecordByIdQuery query);
        Task<IEnumerable<BovinueHealthRecord>> Handle(GetAllBovinueHealthRecordsQuery query);
        Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsByBovinueIdQuery query);
        Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsByBovinueAndDateRangeQuery query);
        Task<IEnumerable<BovinueHealthRecord>> Handle(GetOpenBovinueHealthRecordsByBovinueIdQuery query);
        Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsPagedQuery query);
    }
}