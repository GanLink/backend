using Cortex.Mediator.Infrastructure;
using GanLink.FarmManagement.Application.Exceptions;
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

    /// <summary>
    /// Implementación del handler para eliminar un Farm.
    /// </summary>
    public async Task Handle(DeleteFarmCommand command)
    {
        // 1. Buscar la entidad a eliminar
        var farmToDelete = await repository.FindByIdAsync(command.FarmId);

        // 2. Proteger la lógica (Guard Clause)
        // ¡Nunca borres algo que no existe!
        if (farmToDelete == null)
        {
            // Lanza una excepción específica que la API pueda
            // entender y traducir a un 404 Not Found.
            throw new FarmNotFoundException(command.FarmId);
        }

        // 3. Ejecutar la acción
        repository.Remove(farmToDelete);

        // 4. Persistir los cambios en la transacción
        await unitOfWork.CompleteAsync();
    }
}