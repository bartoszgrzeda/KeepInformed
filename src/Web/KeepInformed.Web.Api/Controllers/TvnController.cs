using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Web.Api.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/Tvn")]
public class TvnController : Controller
{
    private readonly IResponseManager _responseManager;

    public TvnController(IResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    [HttpPost("SynchronizeNewestNews")]
    public async Task<IActionResult> SynchronizeNewestNews()
    {
        var command = new SynchronizeTvnNewestNewsCommand();

        return await _responseManager.SendCommand(command);
    }
}
