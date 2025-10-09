using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    public interface IBovinueHealthRecordRepository : IBaseRepository<BovinueHealthRecord>
    {
        Task<BovinueHealthRecord?> GetByIdAsync(long id);
        Task<ICollection<BovinueHealthRecord>> GetByBovinueIdAsync(long bovinueId);
        Task<ICollection<BovinueHealthRecord>> GetOpenRecordsByBovinueIdAsync(long bovinueId);
        Task<ICollection<BovinueHealthRecord>> GetByDateRangeAsync(long bovinueId, DateTime startDate, DateTime endDate);
        
        // Business rule: check if bovine has active vaccination
        Task<bool> HasActiveVaccinationAsync(long bovinueId, long cattleHealthRecordId);
    }
}