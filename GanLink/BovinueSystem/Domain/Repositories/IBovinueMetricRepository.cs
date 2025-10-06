namespace GanLink.BovinueSystem.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;


public interface IBovinueMetricRepository : IBaseRepository<BovinueMetric>
{
    Task<BovinueMetric?> GetByIdAsync(long id);
    Task<ICollection<BovinueMetric>> GetByBovinueIdAsync(long bovinueId);
    Task<ICollection<BovinueMetric>> GetByBovinueAndDateRangeAsync(long bovinueId, DateTime from, DateTime to);

    // Apoya regla: unicidad por (BovinueId, BovinueMPId, Date)
    Task<bool> ExistsOnInstantAsync(long bovinueId, long bovinueMPId, DateTime at);
}

