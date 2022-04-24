using KeepInformed.Contracts.Authorization.Commands.UserSignIn;
using KeepInformed.Contracts.Authorization.Commands.UserSignUp;
using KeepInformed.Contracts.Authorization.Queries.GetUserJwt;
using KeepInformed.Contracts.Authorization.Queries.GetUsers;
using KeepInformed.Web.Api.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[AllowAnonymous]
[Route("api/User")]
public class UserController : Controller
{
    private readonly IResponseManager _responseManager;

    public UserController(IResponseManager responseManager)
    {
        _responseManager = responseManager;
    }

    [HttpPut("SignIn")]
    [ProducesResponseType(typeof(GetUserJwtQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> SignIn(UserSignInCommand command)
    {
        var signInResult = await _responseManager.SendCommand(command);

        if (signInResult is not JsonResult)
        {
            return signInResult;
        }

        var getJwtQuery = new GetUserJwtQuery()
        {
            Email = command.Email
        };

        return await _responseManager.SendQuery(getJwtQuery);
    }

    [HttpPost("SignUp")]
    public async Task<IActionResult> SignUp(UserSignUpCommand command)
    {
        return await _responseManager.SendCommand(command);
    }

    [HttpGet("Get")]
    [ProducesResponseType(typeof(GetUsersQueryResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Get()
    {
        var query = new GetUsersQuery();

        return await _responseManager.SendQuery(query);
    }
}
