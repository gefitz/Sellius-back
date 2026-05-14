namespace Sellius.API.Domain.Extensions;

public static class CryptoExtension
{
    public static string Hash(this string value) =>
        BCrypt.Net.BCrypt.HashPassword(value);

    public static bool Verify(this string hash, string value) =>
        BCrypt.Net.BCrypt.Verify(value, hash);
}
