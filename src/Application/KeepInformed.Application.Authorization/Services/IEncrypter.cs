namespace KeepInformed.Application.Authorization.Services;

public interface IEncrypter
{
    string GetSalt();
    string GetHash(string value, string salt);
}
