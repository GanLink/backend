using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.BovinueSystem.Domain.Models.Queries;
using GanLink.BovinueSystem.Domain.Repositories;
using GanLink.BovinueSystem.Domain.Services;

namespace GanLink.BovinueSystem.Application.Internal.QueryServices
{
    public class BovinueHealthRecordQueryService : IBovinueHealthRecordQueryService
    {
        private readonly IBovinueHealthRecordRepository _healthRecordRepository;

        public BovinueHealthRecordQueryService(IBovinueHealthRecordRepository healthRecordRepository)
        {
            _healthRecordRepository = healthRecordRepository;
        }

        public async Task<BovinueHealthRecord?> Handle(GetBovinueHealthRecordByIdQuery query)
        {
            var record = await _healthRecordRepository.GetByIdAsync(query.id);
            return (record != null && !record.deleted) ? record : null;
        }

        public async Task<IEnumerable<BovinueHealthRecord>> Handle(GetAllBovinueHealthRecordsQuery query)
        {
            var records = await _healthRecordRepository.ListAsync();
            return records.Where(r => !r.deleted);
        }

        public async Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsByBovinueIdQuery query)
        {
            var records = await _healthRecordRepository.GetByBovinueIdAsync(query.bovinueId);
            return records.Where(r => !r.deleted);
        }

        public async Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsByBovinueAndDateRangeQuery query)
        {
            var records = await _healthRecordRepository.GetByDateRangeAsync(
                query.bovinueId, 
                query.from, 
                query.to);
            return records.Where(r => !r.deleted);
        }

        public async Task<IEnumerable<BovinueHealthRecord>> Handle(GetOpenBovinueHealthRecordsByBovinueIdQuery query)
        {
            var records = await _healthRecordRepository.GetOpenRecordsByBovinueIdAsync(query.bovinueId);
            return records.Where(r => !r.deleted);
        }

        public async Task<IEnumerable<BovinueHealthRecord>> Handle(GetBovinueHealthRecordsPagedQuery query)
        {
            var records = await _healthRecordRepository.ListAsync();
            return records
                .Where(r => !r.deleted)
                .Skip((query.page - 1) * query.pageSize)
                .Take(query.pageSize);
        }
    }
}