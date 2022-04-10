using KeepInformed.Contracts.Tvn.Commands.SynchronizeTvnNewestNews;
using KeepInformed.Contracts.Tvn.Queries.GetTvnNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Route("Tvn")]
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

    [HttpGet("GetNews")]
    public async Task<IActionResult> GetNews()
    {
        var query = new GetTvnNewsQuery();

        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
