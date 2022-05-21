using KeepInformed.Contracts.MasterNews.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Web.Shared.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Admin.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/TvnNews")]
public class TvnNewsController : Controller
{
    private readonly IResponseManager _responseManager;

    public TvnNewsController(IResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    [HttpPut("SynchronizeNewestNews")]
    public async Task<IActionResult> SynchronizeNewestNews()
    {
        var command = new SynchronizeTvnNewestNewsCommand();

        return await _responseManager.SendCommand(command);
    }
}
