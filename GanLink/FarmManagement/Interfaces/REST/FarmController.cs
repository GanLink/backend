using System.Net.Mime;
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
    [SwaggerOperation(Summary = "Gets a Rent according to id", Description = "Gets a Rent according to id",
        OperationId = "GetsRentById")]

    public async Task<ActionResult> GetRentById(int id)
    {
        var getFarmById = new GetFarmByIdQuery(id);
        var result = await farmQueryService.Handle(getFarmById);
        if (result is null) return NotFound();
        var resource = FarmResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    [HttpPost("delete")]
    [SwaggerOperation(Summary = "Delete a Farm according to id", Description = "Delete a Farm according to id",
        OperationId = "DeleteFarmById")]

    public async Task<ActionResult> DeleteFarmById([FromBody]DeleteFarmResource resource)
    {
        var deleteFarmById = DeleteFarmCommandFromResourceAssembler.ToCommandFromEntity(resource);
        await farmCommandService.Handle(deleteFarmById);
        return Ok();
    }
    [HttpGet("user/{userid}")]
    [SwaggerOperation(Summary = "Gets a Farm according to user id", Description = "Gets a Farm according to id",
        OperationId = "GetsFarmById")]

    public async Task<ActionResult> GetFarmByUserId(int userid)
    {
        var getFarmByUserId = new GetFarmByUserId(userid);
        var result = await farmQueryService.Handle(getFarmByUserId);
        if (result is null) return NotFound();
        var resource = FarmResourceFromEntityAssembler.ToResourceFromEntity(result);
        return Ok(resource);
    }
    
}