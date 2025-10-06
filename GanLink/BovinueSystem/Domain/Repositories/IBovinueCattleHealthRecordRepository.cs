namespace GanLink.BovinueSystem.Domain.Repositories;

using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.BovinueSystem.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;



public interface IBovinueCattleHealthRecordRepository : IBaseRepository<BovinueCattleHealthRecord>
{
    Task<BovinueCattleHealthRecord?> GetByIdAsync(long id);
    Task<ICollection<BovinueCattleHealthRecord>> GetByActivityNameAsync(string activityName);
}

