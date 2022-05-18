using KeepInformed.Web.Shared.ResponseManager;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace KeepInformed.Web.Api.Controllers;

[ApiController]
[Route("api/Auth")]
public class AuthController : Controller
{
    private readonly IResponseManager _responseManager;
    private readonly IConfiguration _configuration;

    public AuthController(IResponseManager responseManager, IConfiguration configuration)
    {
        _responseManager = responseManager;
        _configuration = configuration;
    }    

    [AllowAnonymous]
    [HttpGet]
    public IActionResult GetToken()
    {
        return Ok(GenerateJSONWebToken());
    }

    private string GenerateJSONWebToken()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], null, expires: DateTime.Now.AddDays(1), signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
