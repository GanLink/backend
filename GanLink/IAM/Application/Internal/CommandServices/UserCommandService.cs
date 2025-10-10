using System;
using System.Threading.Tasks;
using GanLink.IAM.Application.Internal.OutboundServices;
using GanLink.IAM.Domain.Models.Aggregates;
using GanLink.IAM.Domain.Models.Commands;
using GanLink.IAM.Domain.Repositories;
using GanLink.IAM.Domain.Services;
using GanLink.Shared.Domain.Repositories;

namespace GanLink.IAM.Application.Internal.CommandServices;

public class UserCommandService(IUserRepository userRepository, IUnitOfWork unitOfWork,
    IHashingService hashingService, ITokenService tokenService) : IUserCommandService
{
    public async Task<User?> Handle(SignUpCommand command)
    {
        var user = await userRepository.FindUserByUsername(command.Username);

        if (user != null)
            return null;
        
        var hashedPassword = hashingService.HashPassword(command.Password);

        user = new User(command, hashedPassword);
        

        try
        {
            await userRepository.AddAsync(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while saving user: {ex.Message}");
            throw;
        }

        return user;
    }

    public async Task Handle(DeleteUserCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.id);
        
        if(user == null)
            throw new Exception($"User with id {command.id} doesnt exists.");
        try
        {
            userRepository.Remove(user);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception($"User with id {command.id} doesnt exists.", e);
        }
        
    }

    public async Task<(User? user, string? token)> Handle(SignInCommand command)
    {
        var user = await userRepository.FindUserByUsername(command.username);
        
        if (user == null || !hashingService.VerifyPassword(command.password, user.Password))
            return (null, null);
        var token = tokenService.GenerateToken(user);
        return (user, token);
    }
}