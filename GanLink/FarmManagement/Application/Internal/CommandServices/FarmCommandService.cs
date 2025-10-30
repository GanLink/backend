using Cortex.Mediator.Infrastructure;
using GanLink.FarmManagement.Application.Exceptions;
using GanLink.FarmManagement.Domain.Models.Aggregates;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Repositories;
using GanLink.FarmManagement.Domain.Services;
using System.IO; // ¡Importante!
using System; // ¡Importante!
using System.Threading.Tasks; // ¡Importante!
using GanLink.IAM.Domain.Repositories;
using IUnitOfWork = GanLink.Shared.Domain.Repositories.IUnitOfWork;

namespace GanLink.FarmManagement.Application.Internal.CommandServices;

public class FarmCommandService(IFarmRepository repository, IUserRepository userRepository, IUnitOfWork unitOfWork,IImageStorageService imageStorageService): IFarmCommandService
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
    
    // --- NUEVO MÉTODO IMPLEMENTADO ---
    public async Task<string> Handle(UploadFarmImageCommand command)
    {
        // 1. Validar el archivo
        if (command.ImageFile == null || command.ImageFile.Length == 0)
        {
            throw new ArgumentException("El archivo de imagen no fue proporcionado o está vacío.");
        }

        // 2. Obtener el agregado Farm
        var farm = await repository.FindByIdAsync(command.FarmId);
        if (farm == null)
        {
            throw new FarmNotFoundException(command.FarmId);
        }

        // 3. Generar nombre único
        var uniqueFileName = $"{Guid.NewGuid().ToString()}_{Path.GetExtension(command.ImageFile.FileName)}";

        // 4. Guardar la imagen físicamente (usando el servicio de infraestructura)
        string imageUrl;
        using (var stream = command.ImageFile.OpenReadStream())
        {
            // "imageStorageService" fue inyectado en el constructor
            imageUrl = await imageStorageService.SaveImageAsync(stream, uniqueFileName, "farms");
        }

        // 5. Actualizar el agregado (Dominio)
        try
        {
            farm.SetImageUrl(imageUrl);
        }
        catch (ArgumentException ex)
        {
            // Re-lanzamos como una excepción de aplicación si es necesario
            throw new ApplicationException($"Error al asignar la URL: {ex.Message}", ex);
        }

        // 6. Persistir cambios (Infraestructura)
        await unitOfWork.CompleteAsync();

        return imageUrl;
    }
}