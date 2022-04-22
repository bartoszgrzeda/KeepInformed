using KeepInformed.Contracts.News.Commands.MarkNewsAsSeen;
using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.News.Queries.GetNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
public class NewsController : Controller
{
    private readonly IMediator _mediator;

    public NewsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetNews")]
    [ProducesResponseType(typeof(GetNewsQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetNews()
    {
        var query = new GetNewsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }

    [HttpPut("MarkNewsAsSeen")]
    public async Task<IActionResult> MarkNewsAsSeen(Guid newsId)
    {
        var command = new MarkNewsAsSeenCommand()
        {
            NewsId = newsId
        };

        await _mediator.Send(command);

        return Ok();
    }
}
