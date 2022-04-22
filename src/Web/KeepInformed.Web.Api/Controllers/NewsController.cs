using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Contracts.News.Queries.GetNews;
using KeepInformed.Web.Api.ResponseManager;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
public class NewsController : Controller
{
    private readonly IResponseManager _responseManager;

    public NewsController(IResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    [HttpGet("GetNews")]
    [ProducesResponseType(typeof(GetNewsQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetNews()
    {
        var query = new GetNewsQuery();

        return await _responseManager.SendQuery(query);
    }

    [HttpPut("MarkNewsAsSeen")]
    public async Task<IActionResult> MarkNewsAsSeen(Guid newsId)
    {
        var command = new MarkNewsAsSeenCommand()
        {
            NewsId = newsId
        };

        return await _responseManager.SendCommand(command);
    }
}
