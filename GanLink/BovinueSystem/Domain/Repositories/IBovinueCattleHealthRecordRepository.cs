using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.BovinueSystem.Domain.Repositories
{
    // DATASET - Read only repository
    public interface IBovinueCattleHealthRecordRepository : IBaseRepository<BovinueCattleHealthRecord>
    {
        Task<BovinueCattleHealthRecord?> GetByIdAsync(long id);
        Task<ICollection<BovinueCattleHealthRecord>> GetAllActiveAsync();
        Task<BovinueCattleHealthRecord?> GetByActivityNameAsync(string activityName);
        
        // Get records by frequency type
        Task<ICollection<BovinueCattleHealthRecord>> GetByFrequencyAsync(int frequency);
    }
}