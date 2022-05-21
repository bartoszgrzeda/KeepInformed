using KeepInformed.Common.Jwt;
using System;
using System.Collections.Generic;

namespace KeepInformed.Tests.UnitTests.Mocks;

public class JwtTokenServiceMock : IJwtTokenService
{
    private readonly Dictionary<string, JwtTokenModel> _cache;

    public JwtTokenServiceMock()
    {
        _cache = new Dictionary<string, JwtTokenModel>();
    }

    public JwtTokenModel GenerateJwtToken(Guid userId, string userEmail)
    {
        return new JwtTokenModel()
        {
            ExpirationDate = DateTime.UtcNow,
            Token = "token"
        };
    }

    public JwtTokenModel GetFromCache(string userEmail)
    {
        return _cache[userEmail];
    }

    public void SetInCache(string userEmail, JwtTokenModel jwtToken)
    {
        _cache.Add(userEmail, jwtToken);
    }
}
