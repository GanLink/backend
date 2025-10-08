using Cortex.Mediator.Infrastructure;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Repositories;
using GanLink.FarmManagement.Domain.Services;
using GanLink.IAM.Domain.Repositories;
using IUnitOfWork = GanLink.Shared.Domain.Repositories.IUnitOfWork;

namespace GanLink.FarmManagement.Application.Internal.CommandServices;

public class FarmCommandService(IFarmRepository repository, IUserRepository userRepository, IUnitOfWork unitOfWork): IFarmCommandService
{
    public async Task<Farm?> Handle(CreateFarmCommand command)
    {
        var user = await userRepository.FindByIdAsync(command.UserId);
        if (user == null) throw new Exception("User not found");

        var farm = new Farm(command)
        {
            User = user
        };
        try
        {
            await repository.AddAsync(farm);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception)
        {
            return null;
        }

        return farm;
    }

    public async Task Handle(DeleteFarmCommand command)
    {
        var farmId = await repository.FindByIdAsync(command.farmId);
        if (farmId == null) throw new Exception("Farm not found");

        try
        {
            repository.Remove(farmId);
            await unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new Exception("Error deleting farm", e);
        }
        
    }
}