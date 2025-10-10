using System.Net.Mime;
using System.Threading.Tasks;
using GanLink.IAM.Domain.Services;
using GanLink.IAM.Interfaces.REST.Resources;
using GanLink.IAM.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace GanLink.IAM.Interfaces.REST;

[Authorize]
[ApiController]
[Route("api/v1/[controller]")]
[SwaggerTag("Authorization API Controller")]
[Produces(MediaTypeNames.Application.Json)]
public class AuthorizationController(IUserCommandService userCommandService) : ControllerBase
{
    
    [AllowAnonymous]
    [HttpPost("sign-in")]
    public async Task<IActionResult> SignIn([FromBody] SignInResource signInResource)
    {
        var signInCommand = SignInCommandFromResourceAssembler.ToCommandFromResource(signInResource);
        var authenticatedUser = await userCommandService.Handle(signInCommand);

        if (authenticatedUser.user == null || authenticatedUser.token == null)
            return Unauthorized("NO_USERNAME_FOUND");
                
        var resource = AuthenticatedUserResourceFromEntityAssembler
            .ToResourceFromEntity(authenticatedUser.user, authenticatedUser.token);
        
        return Ok(resource);
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