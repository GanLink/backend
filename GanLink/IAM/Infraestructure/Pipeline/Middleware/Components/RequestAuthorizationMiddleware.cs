using System;
using System.Linq;
using System.Threading.Tasks;
using GanLink.IAM.Application.Internal.OutboundServices;
using GanLink.IAM.Domain.Models.Queries;
using GanLink.IAM.Domain.Services;
using GanLink.IAM.Infraestructure.Pipeline.Middleware.Attributes;
using Microsoft.AspNetCore.Http;

namespace GanLink.IAM.Infraestructure.Pipeline.Middleware.Components;

public class RequestAuthorizationMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context, IUserQueryService userQueryService, ITokenService tokenService)
    {
        Console.WriteLine("Entering Invoke Async");
        
        var anonymousAllow = context.Request.HttpContext.GetEndpoint()!.Metadata
            .Any(m => m.GetType() == typeof(AllowAnonymousAttribute));

        
        Console.WriteLine($"Allow Anonymous : {anonymousAllow}");
        
        if (anonymousAllow)
        {
            Console.WriteLine("Skipping Auth");
            await next(context);
            return;
        }
        
        Console.WriteLine("Entering Authorization Header");
        
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        
        if(token == null)
            throw new Exception("Missing Authorization Header");
        
        var userId = await tokenService.VerifyToken(token);
        
        if (userId == null)
            throw new Exception("Invalid Authorization Header");

        var getUserById = new GetUserByIdQuery(userId.Value);
        
        var user = await userQueryService.Handle(getUserById);
        
        Console.WriteLine($"Succesfull authorization... Updating Content");
        context.Items["User"] = user;
        Console.WriteLine($"Continue Middleware Pipeline");

        await next(context);

    }
}