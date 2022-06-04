using KeepInformed.Common.MultiTenancy;
using KeepInformed.Contracts.TenantNews.Commands.SynchronizeTvnNews;
using KeepInformed.Contracts.TenantNews.IntegrationEvents;
using KeepInformed.Web.Shared.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/News")]
public class NewsController : Controller
{
    private readonly IResponseManager _responseManager;
    private readonly ITenantProvider _tenantProvider;

    public NewsController(IResponseManager responseManager, ITenantProvider tenantProvider)
    {
        _responseManager = responseManager;
        _tenantProvider = tenantProvider;
    }

    [HttpPut("TvnNews/Synchronize")]
    public async Task<IActionResult> SignIn()
    {
        var userId = _tenantProvider.GetUserId();
        var integrationEvent = new TvnNewsScheduledToBeSynchronized()
        {
            UserId = userId.GetValueOrDefault()
        };

        return await _responseManager.SendIntegrationEvent(integrationEvent);
    }
}