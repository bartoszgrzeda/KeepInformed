namespace KeepInformed.Common.Jwt;

public class JwtTokenModel
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
