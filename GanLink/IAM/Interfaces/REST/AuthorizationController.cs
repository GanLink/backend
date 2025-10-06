using System.Net.Mime;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Services;
using GanLink.IAM.Interfaces.REST.Resources;
using GanLink.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GanLink.IAM.Interfaces.REST;

[Route("api/v1/[controller]")]
[SwaggerTag("Authorization API Controller")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthorizationController(IUserCommandService userCommandService) : ControllerBase
{
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);
        
        if (authenticatedUser == null)
            return NotFound();
        
        return Ok(authenticatedUser);
    }


    [AllowAnonymous]
    [HttpPost("sign-up")]
    public async Task<IActionResult> SignUp([FromBody] SignUpResource signUpResource)
    {
        var signUpCommand = SignUpCommandFromResourceAssembler.ToCommandFromResource(signUpResource);
        var user = await userCommandService.Handle(signUpCommand);

        if (user == null)
            return Unauthorized("EMAIL_EXISTS");
        
        return Ok(new { message = "User created successfully" });
    }

}