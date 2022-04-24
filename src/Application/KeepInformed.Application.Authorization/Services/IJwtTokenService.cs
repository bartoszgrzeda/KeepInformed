using KeepInformed.Application.Authorization.Models;
using KeepInformed.Domain.Authorization.Entities;

namespace KeepInformed.Application.Authorization.Services;

public interface IJwtTokenService
{
    JwtTokenModel GenerateJwtToken(User user);
    JwtTokenModel GetFromCache(string userEmail);
    void SetInCache(string userEmail, JwtTokenModel jwtToken);
}
