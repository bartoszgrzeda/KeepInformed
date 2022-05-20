namespace KeepInformed.Contracts.Authorization.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastSignInDate { get; set; }
    public bool IsEmailConfirmed { get; set; }
}
