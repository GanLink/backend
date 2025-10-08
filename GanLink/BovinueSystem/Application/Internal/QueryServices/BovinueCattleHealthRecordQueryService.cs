using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;

namespace GanLink.BovinueSystem.Application.Internal.QueryServices
{
    public class BovinueCattleHealthRecordQueryService : IBovinueCattleHealthRecordQueryService
    {
        private readonly IBovinueCattleHealthRecordRepository _cattleHealthRepository;

        public BovinueCattleHealthRecordQueryService(IBovinueCattleHealthRecordRepository cattleHealthRepository)
        {
            _cattleHealthRepository = cattleHealthRepository;
        }

        public async Task<BovinueCattleHealthRecord?> Handle(GetBovinueCattleHealthRecordByIdQuery query)
        {
            return await _cattleHealthRepository.GetByIdAsync(query.id);
        }

        public async Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetAllBovinueCattleHealthRecordsQuery query)
        {
            return await _cattleHealthRepository.GetAllActiveAsync();
        }

        public async Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetBovinueCattleHealthRecordsByActivityNameQuery query)
        {
            var record = await _cattleHealthRepository.GetByActivityNameAsync(query.activityName);
            return record != null ? new[] { record } : Enumerable.Empty<BovinueCattleHealthRecord>();
        }

        public async Task<IEnumerable<BovinueCattleHealthRecord>> Handle(GetBovinueCattleHealthRecordsPagedQuery query)
        {
            var records = await _cattleHealthRepository.ListAsync();
            return records
                .Skip((query.page - 1) * query.pageSize)
                .Take(query.pageSize);
        }
    }
}