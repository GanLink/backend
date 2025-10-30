using GanLink.FarmManagement.Domain.Models.Aggregates; // Asumo que tus agregados están aquí
using GanLink.FarmManagement.Domain.Repositories; // Para tu IFarmRepository
using GanLink.FarmManagement.Domain.Services; // Para IImageStorageService
using GanLink.Shared.Domain.Repositories; // <-- ¡NUEVO! Importa tu IUnitOfWork
using Microsoft.AspNetCore.Http; // Para IFormFile
using System;
using System.IO;
using System.Threading.Tasks;

namespace GanLink.FarmManagement.Application.Internal.CommandServices;

// (¡OJO! Asumo que no implementa una interfaz, igual que tu FarmCommandService,
// pero si tienes una interfaz, asegúrate de implementarla)
public class FarmImageUploadService
{
    private readonly IFarmRepository _farmRepository;
    private readonly IImageStorageService _imageStorageService;
    private readonly IUnitOfWork _unitOfWork; // <-- ¡NUEVO!

    // Inyectamos IUnitOfWork en el constructor
    public FarmImageUploadService(
        IFarmRepository farmRepository, 
        IImageStorageService imageStorageService, 
        IUnitOfWork unitOfWork) // <-- ¡NUEVO!
    {
        _farmRepository = farmRepository;
        _imageStorageService = imageStorageService;
        _unitOfWork = unitOfWork; // <-- ¡NUEVO!
    }

    public async Task<string> UploadImageForFarmAsync(int farmId, IFormFile imageFile)
    {
        // 1. Validar el archivo de entrada
        if (imageFile == null || imageFile.Length == 0)
        {
            throw new ArgumentException("El archivo de imagen no fue proporcionado o está vacío.", nameof(imageFile));
        }

        // 2. Obtener el agregado Farm (EF Core empieza a rastrearlo)
        var farm = await _farmRepository.GetByIdAsync(farmId); 
        if (farm == null)
        {
            throw new KeyNotFoundException($"La granja con ID {farmId} no fue encontrada.");
        }

        // 3. Generar un nombre único para el archivo
        var uniqueFileName = $"{Guid.NewGuid().ToString()}_{Path.GetExtension(imageFile.FileName)}";

        // 4. Guardar la imagen físicamente usando el servicio de infraestructura
        string imageUrl;
        using (var stream = imageFile.OpenReadStream())
        {
            imageUrl = await _imageStorageService.SaveImageAsync(stream, uniqueFileName, "farms"); 
        }

        // 5. Actualizar el agregado Farm (EF Core marca la entidad como "Modified")
        try
        {
            farm.SetImageUrl(imageUrl);
        }
        catch (ArgumentException ex)
        {
            throw new ApplicationException($"Error al asignar la URL de la imagen a la granja: {ex.Message}", ex);
        }

        // 6. Persistir los cambios usando UnitOfWork (¡CAMBIO CLAVE!)
        await _unitOfWork.CompleteAsync(); // <-- ¡ARREGLADO!

        return imageUrl;
    }
}