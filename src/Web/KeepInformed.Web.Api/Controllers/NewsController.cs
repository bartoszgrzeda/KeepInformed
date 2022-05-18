using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Contracts.News.Queries.GetNews;
using KeepInformed.Web.Shared.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/News")]
public class NewsController : Controller
{
    private readonly IResponseManager _responseManager;

    public NewsController(IResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    [HttpGet("Get")]
    [ProducesResponseType(typeof(GetNewsQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetNewsQuery();

        return await _responseManager.SendQuery(query);
    }

    [HttpPut("MarkAsSeen")]
    public async Task<IActionResult> MarkAsSeen(Guid newsId)
    {
        var command = new MarkNewsAsSeenCommand()
        {
            NewsId = newsId
        };

        return await _responseManager.SendCommand(command);
    }
}
