using System.Security.Cryptography;
using System.Text;

namespace RecipesApp.Application.Services;

public class PasswordEncryptionService
{
    private const string _KEY = "net";

    public string Encrypt(string password)
    {
        password = $"{password}{_KEY}";
        byte[] bytes = Encoding.UTF8.GetBytes(password);
        byte[] hash = SHA512.HashData(bytes);

        return BuildString(hash);
    }
    private string BuildString(byte[] bytes)
    {
        StringBuilder sb = new();

        foreach (byte b in bytes)
            sb.Append(b.ToString("x2"));
        return sb.ToString();
    }
}
