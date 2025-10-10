using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.IAM.Domain.Repositories;

public interface IUserRepository : IBaseRepository<User>
{
    Task<User?> GetUserByIdAsync(int id);
    Task<User?> FindUserByEmail(string email);
    Task<User?> FindUserByUsername(string username);
    Task<List<Farm>> FindUserFarmById(int id);
}