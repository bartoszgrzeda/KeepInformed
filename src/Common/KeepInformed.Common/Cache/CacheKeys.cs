namespace KeepInformed.Common.Cache;

public static class CacheKeys
{
    public static string UserJwt(string email) => $"Jwt_{email}";
}
