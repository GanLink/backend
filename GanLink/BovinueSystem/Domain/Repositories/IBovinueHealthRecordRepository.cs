namespace GanLink.BovinueSystem.Domain.Repositories;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;



public interface IBovinueHealthRecordRepository : IBaseRepository<BovinueHealthRecord>
{
    Task<BovinueHealthRecord?> GetByIdAsync(long id);
    Task<ICollection<BovinueHealthRecord>> GetByBovinueIdAsync(long bovinueId);
    Task<ICollection<BovinueHealthRecord>> GetOpenByBovinueIdAsync(long bovinueId);

    // Apoya regla: no permitir periodos solapados para una misma plantilla
    Task<bool> HasOverlapAsync(long bovinueId, long bovinueCHRId, DateTime startDate, DateTime? endDate);
}

