using KeepInformed.Contracts.News.Commands.Tvn.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.News.Queries.GetNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
public class TvnController : Controller
{
    private readonly IMediator _mediator;

    public TvnController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("SynchronizeNewestNews")]
    public async Task<IActionResult> SynchronizeNewestNews()
    {
        var command = new SynchronizeTvnNewestNewsCommand();

        await _mediator.Send(command);

        return Ok();
    }
}
