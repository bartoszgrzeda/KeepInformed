namespace KeepInformed.Contracts.Authorization.Queries.GetUserJwt;

public class GetUserJwtQueryResponse
{
    public string Token { get; set; }
    public DateTime ExpirationDate { get; set; }
}
