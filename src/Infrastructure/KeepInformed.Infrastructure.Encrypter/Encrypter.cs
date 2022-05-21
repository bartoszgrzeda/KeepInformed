using KeepInformed.Common.Encrypter;
using System.Security.Cryptography;

namespace KeepInformed.Infrastructure.Encrypter;

public class Encrypter : IEncrypter
{
    private const int _saltSize = 40;
    private const int _deriveBytesIterations = 10000;
    public string GetHash(string value, string salt)
    {
        var rfc289 = new Rfc2898DeriveBytes(value, GetBytes(salt), _deriveBytesIterations);

        return Convert.ToBase64String(rfc289.GetBytes(_saltSize));
    }

    public string GetSalt()
    {
        var saltBytes = new byte[_saltSize];
        var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(saltBytes);

        return Convert.ToBase64String(saltBytes);
    }

    private static byte[] GetBytes(string value)
    {
        var bytes = new byte[value.Length * sizeof(char)];
        Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

        return bytes;
    }
}
