using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Aggregates;

namespace GanLink.IAM.Application.Internal.OutboundServices;

public interface ITokenService
{
    string GenerateToken(User user);
    Task<int?> VerifyToken(string token);
}