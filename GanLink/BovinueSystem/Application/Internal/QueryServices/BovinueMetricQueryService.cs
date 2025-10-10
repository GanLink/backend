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
    public class BovinueMetricQueryService : IBovinueMetricQueryService
    {
        private readonly IBovinueMetricRepository _metricRepository;

        public BovinueMetricQueryService(IBovinueMetricRepository metricRepository)
        {
            _metricRepository = metricRepository;
        }

        public async Task<BovinueMetric?> Handle(GetBovinueMetricByIdQuery query)
        {
            var metric = await _metricRepository.GetByIdAsync(query.id);
            return (metric != null && !metric.deleted) ? metric : null;
        }

        public async Task<IEnumerable<BovinueMetric>> Handle(GetAllBovinueMetricsQuery query)
        {
            var metrics = await _metricRepository.ListAsync();
            return metrics.Where(m => !m.deleted);
        }

        public async Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByBovinueIdQuery query)
        {
            var metrics = await _metricRepository.GetByBovinueIdAsync(query.bovinueId);
            return metrics.Where(m => !m.deleted);
        }

        public async Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByDateRangeQuery query)
        {
            var metrics = await _metricRepository.GetByDateRangeAsync(
                query.bovinueId,
                query.startDate,
                query.endDate);
            return metrics.Where(m => !m.deleted);
        }

        public async Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsByParameterQuery query)
        {
            var metrics = await _metricRepository.GetByParameterIdAsync(
                query.bovinueId,
                query.parameterId);
            return metrics.Where(m => !m.deleted);
        }

        public async Task<IEnumerable<BovinueMetric>> Handle(GetBovinueMetricsPagedQuery query)
        {
            var metrics = await _metricRepository.ListAsync();
            return metrics
                .Where(m => !m.deleted)
                .Skip((query.pageNumber - 1) * query.pageSize)
                .Take(query.pageSize);
        }
    }
}
