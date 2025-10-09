using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;

namespace GanLink.BovinueSystem.Domain.Services
{
    // DATASET - Query only service
    public interface IBovinueCattleHealthRecordQueryService
    {
        Task<BovinueCattleHealthRecord?> Handle(GetBovinueCattleHealthRecordByIdQuery query);
        Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetAllBovinueCattleHealthRecordsQuery query);
        Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetBovinueCattleHealthRecordsByActivityNameQuery query);
        Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetBovinueCattleHealthRecordsPagedQuery query);
    }
}