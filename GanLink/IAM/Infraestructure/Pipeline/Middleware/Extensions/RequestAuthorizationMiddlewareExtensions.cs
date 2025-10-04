using GanLink.IAM.Infraestructure.Pipeline.Middleware.Components;
using Microsoft.AspNetCore.Builder;

namespace GanLink.IAM.Infraestructure.Pipeline.Middleware.Extensions;

public static class RequestAuthorizationMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestAuthorizationMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<RequestAuthorizationMiddleware>();
    }
}