using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.FarmManagement.Domain.Repositories;

public interface IFarmRepository : IBaseRepository<Farm>
{
    Task<Farm?> GetByIdAsync(int id);
    
    Task<Farm?> GetByUserId(int userId);
    
}