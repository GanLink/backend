using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Models.Commands;
using GanLink.IAM.Domain.Models.Queries;
using GanLink.IAM.Domain.Services;
using GanLink.IAM.Interfaces.REST.Resources;
using GanLink.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GanLink.IAM.Interfaces;

[Route("api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[Tags("User API")]
public class UserController(
    IUserCommandService commandService,
    IUserQueryService userQueryService
    ) : ControllerBase
{
    [HttpPost]
    
    public async Task<IActionResult> CreateUser([FromBody] SignUpResource resource)
    {
        var createUserCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(resource);
        try
        {
            var result = await commandService.Handle(createUserCommand);

            if (result == null) return BadRequest();

            return CreatedAtAction(nameof(GetUserById), new { id = result.Id },
                UserResourceFromEntityAssembler.ToResourceFromEntity(result));
        }
        catch (Exception e)
        {
            if (e.Message == "EMAIL_EXISTS")
            {
                return Conflict(new { code = "EMAIL_EXISTS", message = "El email ya está registrado." });
            }

            return StatusCode(500, "Error interno del servidor.");
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById([FromRoute] int id)
    {
        var getByIdQuery = new GetUserByIdQuery(id);
        var result = await  userQueryService.Handle(getByIdQuery);
        
        if (result == null) return NotFound();
        
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);
        
        return Ok(resource);
    }
    [AllowAnonymous]
    [HttpGet("email/{email}")]
    public async Task<IActionResult> GetUserByEmail([FromRoute] string email)
    {
        var getUserByEmail = new GetUserByEmail(email);
        var result = await  userQueryService.Handle(getUserByEmail);
        
        if (result == null) return Ok();
        
        var resource = UserResourceFromEntityAssembler.ToResourceFromEntity(result);

        return NotFound();
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResource>>> GetUsers()
    {
        var getAllQuery = new GetAllUsersQuery();
        var result = await userQueryService.Handle(getAllQuery);

        foreach (var user in result)
        {
            if (user != null) UserResourceFromEntityAssembler.ToResourceFromEntity(user);
        }
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id)
    {
        var deleteUserCommand = new DeleteUserCommand(id);
        await commandService.Handle(deleteUserCommand);
    
        return NoContent();
    }

    [HttpGet("{id}/farms")]
    public async Task<IActionResult> GetFarms([FromRoute] int id)
    {
        var getFarmsQuery = new GetUserFarmsById(id);
        var farms  = await userQueryService.Handle(getFarmsQuery);
        
        if(farms.Count == 0)
            return NoContent();
        return Ok(farms);
    }
}