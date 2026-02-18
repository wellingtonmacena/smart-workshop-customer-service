using System.Security.Cryptography;
using System.Text;

namespace SmartWorkshop.Core.Domain.ValueObjects;

public record Password
{
    private Password() { }

    private Password(string value, bool isHashed = false)
    {
        Value = isHashed ? value : HashPassword(value);
    }

    public string Value { get; private set; } = string.Empty;

    public static implicit operator Password(string value) => new Password(value);
    public static implicit operator string(Password password) => password.Value;

    public static Password FromHash(string hash) => new Password(hash, isHashed: true);

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(hashedBytes);
    }

    public bool Verify(string password)
    {
        var hashedInput = HashPassword(password);
        return hashedInput == Value;
    }
}
