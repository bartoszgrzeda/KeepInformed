using KeepInformed.Common.Cache;
using KeepInformed.Common.Jwt;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KeepInformed.Infrastructure.Jwt;
public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;
    private readonly IMemoryCache _cache;

    public JwtTokenService(IConfiguration configuration, IMemoryCache cache)
    {
        _configuration = configuration;
        _cache = cache;
    }

    public JwtTokenModel GenerateJwtToken(Guid userId, string userEmail)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        var expirationDate = DateTime.UtcNow.AddDays(1);

        var claims = new Claim[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, userEmail)
        };

        var jwtToken = new JwtSecurityToken(_configuration["Jwt:Issuer"], _configuration["Jwt:Issuer"], claims, expires: expirationDate, signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

        return new JwtTokenModel()
        {
            Token = token,
            ExpirationDate = expirationDate
        };
    }

    public JwtTokenModel GetFromCache(string userEmail)
    {
        var email = userEmail.ToLower();
        var cacheKey = CacheKeys.UserJwt(email);

        return _cache.Get<JwtTokenModel>(cacheKey);
    }

    public void SetInCache(string userEmail, JwtTokenModel jwtToken)
    {
        var cacheKey = CacheKeys.UserJwt(userEmail);
        _cache.Set(cacheKey, jwtToken, TimeSpan.FromSeconds(5));
    }
}
