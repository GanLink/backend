using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    public interface IBovinueRepository : IBaseRepository<Bovinue>
    {
        Task<Bovinue?> GetByIdAsync(long id);
        Task<ICollection<Bovinue>> GetByFarmIdAsync(long farmId);
        
        // Business rule: prevent transfers/deletions with open health records
        Task<bool> HasOpenHealthRecordsAsync(long bovinueId);
    }
}