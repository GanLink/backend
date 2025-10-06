using System.Collections.Generic;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Queries;

namespace GanLink.IAM.Domain.Services;

public interface IUserQueryService
{
    Task<IEnumerable<User?>> Handle(GetAllUsersQuery query);
    Task<User?> Handle(GetUserByIdQuery query);
    Task<User?> Handle(GetUserByEmail query);
}