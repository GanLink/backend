using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    public interface IBovinueMetricRepository : IBaseRepository<BovinueMetric>
    {
        Task<BovinueMetric?> GetByIdAsync(long id);
        Task<ICollection<BovinueMetric>> GetByBovinueIdAsync(long bovinueId);
        Task<ICollection<BovinueMetric>> GetByParameterIdAsync(long bovinueId, long parameterId);
        Task<ICollection<BovinueMetric>> GetByDateRangeAsync(long bovinueId, DateTime startDate, DateTime endDate);
        
        // Business rule: check duplicate metrics for same date
        Task<bool> ExistsForDateAsync(long bovinueId, long parameterId, DateTime date);
        
        // Get latest metric value for a parameter
        Task<BovinueMetric?> GetLatestByParameterAsync(long bovinueId, long parameterId);
    }
}