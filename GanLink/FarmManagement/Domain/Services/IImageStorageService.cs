using System.IO;
using System.Threading.Tasks;

namespace GanLink.FarmManagement.Domain.Services;

public interface IImageStorageService
{
    /// <summary>
    /// Guarda un archivo de imagen en el almacenamiento.
    /// </summary>
    /// <param name="imageStream">El stream del archivo a guardar.</param>
    /// <param name="fileName">El nombre único del archivo (con extensión).</param>
    /// <param name="containerName">El "directorio" lógico o contenedor (ej: "farms", "users").</param>
    /// <returns>La URL o ruta de acceso pública al archivo guardado.</returns>
    Task<string> SaveImageAsync(Stream imageStream, string fileName, string containerName);
}