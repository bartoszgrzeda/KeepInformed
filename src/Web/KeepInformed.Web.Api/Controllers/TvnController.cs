using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.News.Queries.GetNews;
using KeepInformed.Web.Api.ResponseManager;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
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
