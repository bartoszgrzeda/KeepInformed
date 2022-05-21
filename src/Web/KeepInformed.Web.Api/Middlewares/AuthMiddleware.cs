using KeepInformed.Common.MultiTenancy;
using System.Security.Claims;

namespace KeepInformed.Web.Api.Middlewares;

public class AuthMiddleware
{
    private readonly RequestDelegate _next;

    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var identity = context.User.Identity as ClaimsIdentity;
        var isAuthenticated = identity?.IsAuthenticated ?? false;
        var claim = identity?.FindFirst(ClaimTypes.NameIdentifier);

        if (claim == null || !isAuthenticated)
        {
            await _next(context);
            return;
        }

        var tenantProvider = context.RequestServices.GetRequiredService<ITenantProvider>();
        var userId = new Guid(claim.Value);

        tenantProvider.SetUserId(userId);

        await _next(context);
    }
}
