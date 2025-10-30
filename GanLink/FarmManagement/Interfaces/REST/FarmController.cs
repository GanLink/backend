using System.Net.Mime;
using GanLink.FarmManagement.Application.Exceptions;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Models.Queries;
using GanLink.FarmManagement.Domain.Services;
using GanLink.FarmManagement.Interfaces.REST.Resources;
using GanLink.FarmManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Threading; // ¡Importante!
using System.Collections.Generic; // ¡Importante!
using System.Threading.Tasks; // ¡Importante!
using Microsoft.AspNetCore.Http; // ¡Importante!
namespace GanLink.FarmManagement.Interfaces.REST;

[ApiController]
[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("Farm API")]
public class FarmController(
    IFarmCommandService farmCommandService,
    IFarmQueryService farmQueryService
    ) : ControllerBase
{
    [HttpPost]
    [SwaggerOperation(
        Summary = "Create a new farm",
        Description = "Create a new farm",
        OperationId = "CreateFarm")]
    [SwaggerResponse(201, "The farm was created successfully.")]
    [SwaggerResponse(400, "The farm was not created successfully.")]
    public async Task<IActionResult> CreateFarm([FromBody] CreateFarmResource resource)
    {
        var createFarmCommand = CreateFarmCommandFromResourceAssembler.ToCommandFromResource(resource);
        var result = await farmCommandService.Handle(createFarmCommand);

        if (result == null)
            return BadRequest();
        
        return CreatedAtAction(nameof(CreateFarm), new { id = result.Id }, FarmResourceFromEntityAssembler.ToResourceFromEntity(result));
    }
    
    [HttpGet("{id}")]
    [SwaggerOperation(Summary = "Gets a Farm according to id", Description = "Gets a Farm according to id",
        OperationId = "GetsFarmById")]

    public async Task<ActionResult> GetFarmById(int id)
    {
        var getFarmById = new GetFarmByIdQuery(id);
        var result = await farmQueryService.Handle(getFarmById);
        if (result is null) return NotFound();
        var resource = FarmResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }



    [HttpDelete("{id}")]


    [ProducesResponseType(StatusCodes.Status204NoContent)]

    [ProducesResponseType(StatusCodes.Status404NotFound)]

    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFarm(int id)
    {
        try
        {
            var command = new DeleteFarmCommand(id);
            await farmCommandService.Handle(command);

            // Esta llamada se mapea al Status204NoContent
            return NoContent(); 
        }
        catch (FarmNotFoundException ex)
        {
            // Esta llamada se mapea al Status404NotFound
            return NotFound(ex.Message); 
        }
        catch (Exception ex)
        {
            // Loguea tu excepción (ex) aquí...
        
            // Esto se mapea al Status500InternalServerError
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado.");
        }
    }
    
    [HttpGet("users/{userId:int}/farms")]
    public async Task<ActionResult<IEnumerable<FarmResource>>> ListByUserIdAsync(
        int userId, CancellationToken ct = default)
    {
        var farms = await farmQueryService.Handle(new GetAllFarmsByUserIdQuery(userId));

        if (farms is null) return NotFound();              // null => problema aguas arriba
        if (farms.Count == 0) return Ok(Array.Empty<FarmResource>());

        var resources = farms.Select(FarmResourceFromEntityAssembler.ToResourceFromEntity);
        return Ok(resources);
    }
    
    // --- NUEVO ENDPOINT PARA SUBIR IMAGEN ---
    
    /// <summary>
    /// Sube o actualiza la imagen de perfil de una granja.
    /// </summary>
    /// <remarks>
    /// Esta operación acepta un archivo de imagen (multipart/form-data)
    /// y lo asocia con la granja especificada por el ID.
    /// </remarks>
    /// <param name="id">El ID de la granja (int).</param>
    /// <param name="imageFile">El archivo de imagen a subir (IFormFile).</param>
    [HttpPost("{id:int}/image")]
    [Consumes("multipart/form-data")] // Especifica que este endpoint espera un formulario con archivos
    [SwaggerOperation(
        Summary = "Upload an image for a farm",
        Description = "Uploads or replaces the profile image for a specific farm.",
        OperationId = "UploadFarmImage")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)] // Devuelve un JSON con la URL
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UploadFarmImage(int id,  IFormFile imageFile)
    {
        try
        {
            // 1. Creamos el comando
            var command = new UploadFarmImageCommand(id, imageFile);
            
            // 2. Lo pasamos al servicio de comandos
            var imageUrl = await farmCommandService.Handle(command);

            // 3. Devolvemos la URL
            return Ok(new { imageUrl = imageUrl });
        }
        catch (FarmNotFoundException ex)
        {
            // Si el ID de la granja no existe
            return NotFound(ex.Message);
        }
        catch (ArgumentException ex)
        {
            // Si el archivo es nulo, vacío, o hay un error de validación
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            // Loguea tu excepción (ex) aquí...
            return StatusCode(StatusCodes.Status500InternalServerError, "Ocurrió un error inesperado al procesar la imagen.");
        }
    }
    
}