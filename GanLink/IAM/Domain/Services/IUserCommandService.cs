using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Commands;

namespace GanLink.IAM.Domain.Services;

public interface IUserCommandService
{
    Task<User?> Handle(SignUpCommand command);
    Task Handle(DeleteUserCommand command);
    Task<(User? user, string? token)> Handle(SignInCommand command);
}