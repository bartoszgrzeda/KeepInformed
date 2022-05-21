namespace KeepInformed.Common.Jwt;

public interface IJwtTokenService
{
    JwtTokenModel GenerateJwtToken(Guid userId, string userEmail);
    JwtTokenModel GetFromCache(string userEmail);
    void SetInCache(string userEmail, JwtTokenModel jwtToken);
}
