using System.Security.Cryptography;
using System.Text;

namespace AdvertisementsBoard.Application.AppServices.PasswordHasher;

/// <inheritdoc/>>
public class PasswordHasherService : IPasswordHasherService
{
    /// <inheritdoc/>
    public string HashPassword(string password)
    {
        // Вычисляется хэш, записывается в массив байтов.
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        // Преобразует байты в строку.
        var builder = new StringBuilder();
        foreach (var t in bytes) builder.Append(t.ToString("x2"));

        return builder.ToString();
    }

    /// <inheritdoc/>
    public bool VerifyPassword(string hashedPassword, string passwordToCheck)
    {
        // Вычисляется хэш, записывается в массив байтов.
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordToCheck));

        // Преобразует байты в строку.
        var builder = new StringBuilder();
        foreach (var t in bytes) builder.Append(t.ToString("x2"));

        // Сравнивает два хеша.
        return hashedPassword == builder.ToString();
    }
}