namespace KeepInformed.Common.Encrypter;

public interface IEncrypter
{
    string GetSalt();
    string GetHash(string value, string salt);
}
