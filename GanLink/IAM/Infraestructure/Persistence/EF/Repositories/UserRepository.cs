using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Repositories;
using GanLink.Shared.Infrastructure.Persistence.EFC.Configuration;
using GanLink.Shared.Infrastructure.Persistence.EFC.Repositories;
using Microsoft.EntityFrameworkCore;

namespace GanLink.IAM.Infraestructure.Persistence.EF.Repositories;

public class UserRepository(GanLinkDBContext context) : BaseRepository<User>(context), IUserRepository
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

    public async Task<IEnumerable<User>> FindUsersByEmail(string typeUser)
    {
        return await Context.Set<User>().Where(f => f.TypeUser == typeUser).ToListAsync();
    }

    public async Task<User?> FindUserByUsername(string username)
    {
        return await  Context.Set<User>().FirstOrDefaultAsync(f => f.Username == username);
    }
}