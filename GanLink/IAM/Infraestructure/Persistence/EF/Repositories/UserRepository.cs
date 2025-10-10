using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GanLink.IAM.Infraestructure.Persistence.EF.Repositories;

public class UserRepository(GanLinkDbContext context) : BaseRepository<User>(context), IUserRepository
{
    public async Task<User?> GetUserByIdAsync(int id)
    {
        return await Context.Set<User>()
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<User?> FindUserByEmail(string email)
    {
        return await Context.Set<User>().FirstOrDefaultAsync(f => f.Email == email);
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        return await  Context.Set<User>().FirstOrDefaultAsync(f => f.Username == username);
    }

    public async Task<List<Farm>> FindUserFarmById(int id)
    {
        return await Context.Set<Farm>().Include(u => u.User).Where(f => f.User.Id == id).ToListAsync();
    }
}