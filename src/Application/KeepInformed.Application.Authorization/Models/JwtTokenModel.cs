namespace KeepInformed.Application.Authorization.Models;

public class JwtTokenModel
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
