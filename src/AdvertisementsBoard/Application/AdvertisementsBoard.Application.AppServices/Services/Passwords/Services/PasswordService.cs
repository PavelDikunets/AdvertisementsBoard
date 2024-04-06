using System.Security.Cryptography;
using System.Text;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.PasswordErrorExceptions;

namespace AdvertisementsBoard.Application.AppServices.Services.Passwords.Services;

/// <inheritdoc />
public class PasswordService : IPasswordService
{
    /// <inheritdoc />
    public string HashPassword(string password)
    {
        // Вычисляется хэш, записывается в массив байтов.
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

        // Преобразует байты в строку.
        var builder = new StringBuilder();
        foreach (var t in bytes) builder.Append(t.ToString("x2"));

        return builder.ToString();
    }

    /// <inheritdoc />
    public void ComparePasswordHashWithPassword(string hashedPassword, string passwordToCheck)
    {
        // Вычисляется хэш, записывается в массив байтов.
        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(passwordToCheck));

        // Преобразует байты в строку.
        var builder = new StringBuilder();
        foreach (var t in bytes) builder.Append(t.ToString("x2"));

        // Сравнивает два хеша.
        var success = hashedPassword == builder.ToString();

        if (!success) throw new InvalidSignInCredentialsException();
    }

    /// <inheritdoc />
    public void ComparePasswords(string password1, string password2)
    {
        var success = string.Equals(password1, password2);

        if (!success) throw new PasswordMismatchException();
    }
}