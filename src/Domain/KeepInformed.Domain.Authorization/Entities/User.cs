using KeepInformed.Common.Domain.Entities;

namespace KeepInformed.Domain.Authorization.Entities;

public class User : BaseEntity
{
    public string Email { get; private set; }
    public string Password { get; private set; }
    public string Salt { get; private set; }
    public DateTime? LastSignInDate { get; private set; }
    public bool IsEmailConfirmed { get; private set; }

    public User(Guid id, string email, string password, string salt) : base(id)
    {
        Email = email;
        Password = password;
        Salt = salt;
        IsEmailConfirmed = false;
    }

    public void SetEmail(string email)
    {
        if (Email == email)
        {
            return;
        }

        Email = email;
    }

    public void SetPassword(string password)
    {
        if (Password == password)
        {
            return;
        }

        Password = password;
    }

    public void SetSalt(string salt)
    {
        if (Salt == salt)
        {
            return;
        }

        Salt = salt;
    }

    public void SetLastSignInDate(DateTime lastSingInDate)
    {
        if (LastSignInDate == lastSingInDate)
        {
            return;
        }

        LastSignInDate = lastSingInDate;
    }

    public void SetIsEmailConfirmed(bool isEmailConfirmed)
    {
        if (IsEmailConfirmed == isEmailConfirmed)
        {
            return;
        }

        IsEmailConfirmed = isEmailConfirmed;
    }
}
