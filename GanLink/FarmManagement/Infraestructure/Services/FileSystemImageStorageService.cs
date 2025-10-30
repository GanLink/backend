using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http; // Para IHttpContextAccessor
using GanLink.FarmManagement.Domain.Services; // Importa la interfaz

namespace GanLink.FarmManagement.Infraestructure.Services;

public class FileSystemImageStorageService: IImageStorageService
{
    private readonly IWebHostEnvironment _env; // Para acceder a wwwroot
    private readonly IHttpContextAccessor _httpContextAccessor; // Para construir la URL base

    public FileSystemImageStorageService(IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
    {
        _env = env;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<string> SaveImageAsync(Stream imageStream, string fileName, string containerName)
    {
        // Ruta donde se guardarán las imágenes: wwwroot/images/{containerName}
        // Ejemplo: wwwroot/images/farms
        var uploadPath = Path.Combine(_env.WebRootPath, "images", containerName);

        // Crear el directorio si no existe
        if (!Directory.Exists(uploadPath))
        {
            Directory.CreateDirectory(uploadPath);
        }

        // Ruta completa del archivo en el servidor
        var filePath = Path.Combine(uploadPath, fileName);

        // Copiar el stream de la imagen al archivo físico
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await imageStream.CopyToAsync(fileStream);
        }

        // Construir la URL pública para acceder a la imagen
        // Necesitamos el contexto HTTP para obtener el dominio de la API
        var request = _httpContextAccessor.HttpContext?.Request;
        if (request == null)
        {
            // Esto no debería pasar en un contexto HTTP, pero es una buena práctica para prevenir NRE
            throw new InvalidOperationException("No se pudo acceder al contexto HTTP para construir la URL de la imagen.");
        }

        var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
        var imageUrl = $"{baseUrl}/images/{containerName}/{fileName}";

        return imageUrl;
    }
}