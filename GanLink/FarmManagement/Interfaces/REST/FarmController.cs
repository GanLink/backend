using System.Net.Mime;
using GanLink.FarmManagement.Application.Exceptions;
using GanLink.FarmManagement.Domain.Models.Commands;
using GanLink.FarmManagement.Domain.Models.Queries;
using GanLink.FarmManagement.Domain.Services;
using GanLink.FarmManagement.Interfaces.REST.Resources;
using GanLink.FarmManagement.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

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
    
}