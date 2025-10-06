using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Queries;
using GanLink.IAM.Domain.Repositories;
using GanLink.IAM.Domain.Services;

namespace GanLink.IAM.Application.Internal.QueryServices;

public class UserQueryService(IUserRepository userRepository) : IUserQueryService
{
    public async Task<IEnumerable<User?>> Handle(GetAllUsersQuery query)
    {
        return await userRepository.ListAsync();
    }

    public async Task<User?> Handle(GetUserByIdQuery query)
    {
        return await userRepository.FindByIdAsync(query.Id);
    }

    public async Task<User?> Handle(GetUserByEmail query)
    {
        return await userRepository.FindUserByEmail(query.email);
    }
    
    public async Task<User?> Handle(GetUserByUsername query)
    {
        return await userRepository.FindUserByUsername(query.username);
    }
}