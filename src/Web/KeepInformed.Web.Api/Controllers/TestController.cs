using KeepInformed.Contracts.Tvn.Queries.GetTvnNewestNews;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Route("Test")]
public class TestController : Controller
{
    private readonly IMediator _mediator;

    public TestController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("TvnNewestNews")]
    [ProducesResponseType(typeof(GetTvnNewestNewsQueryResponse), 200)]
    public async Task<IActionResult> GetTvnNewestNews()
    {
        var query = new GetTvnNewestNewsQuery();
        var result = await _mediator.Send(query);

        return Ok(result);
    }
}
