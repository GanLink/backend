using GanLink.IAM.Application.Internal.OutboundServices;

using BCryptNet = BCrypt.Net.BCrypt;


namespace GanLink.IAM.Infraestructure.Hashing.BCrypt.Services;

public class HashingService : IHashingService
{
    public string HashPassword(string password)
    {
        return  BCryptNet.HashPassword(password);
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        return BCryptNet.Verify(password, hashedPassword);
    }
}