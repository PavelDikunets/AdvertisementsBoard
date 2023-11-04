namespace AdvertisementsBoard.Application.AppServices.Passwords.Services;

/// <summary>
///     Сервис для работы с паролями.
/// </summary>
public interface IPasswordService
{
    /// <summary>
    ///     Хэширует пароль.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <returns>Строка с хэшем пароля.</returns>
    string HashPassword(string password);

    /// <summary>
    ///     Сравнивает два хэша.
    /// </summary>
    /// <param name="hashedPassword">Захешированный пароль.</param>
    /// <param name="passwordToCheck">Пароль.</param>
    /// <returns>Возвращает true, если хэши совпадают, и false в противном случае.</returns>
    bool VerifyPassword(string hashedPassword, string passwordToCheck);

    /// <summary>
    ///     Сравнивает два пароля.
    /// </summary>
    /// <param name="password1">Первый пароль.</param>
    /// <param name="password2">Второй пароль.</param>
    /// <returns>Возвращает true, если пароли совпадают, и false в противном случае.</returns>
    bool ComparePasswords(string password1, string password2);
}