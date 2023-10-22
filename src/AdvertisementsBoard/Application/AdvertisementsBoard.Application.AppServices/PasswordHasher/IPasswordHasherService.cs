namespace AdvertisementsBoard.Application.AppServices.PasswordHasher;

/// <summary>
/// Сервис для работы с паролями.
/// </summary>
public interface IPasswordHasherService
{
    /// <summary>
    /// Хэширует пароль.
    /// </summary>
    /// <param name="password">Пароль.</param>
    /// <returns>Строка с хэшем пароля.</returns>
    string HashPassword(string password);

    /// <summary>
    /// Сравнивает два хэша.
    /// </summary>
    /// <param name="hashedPassword">Захешированный пароль.</param>
    /// <param name="passwordToCheck">Пароль.</param>
    /// <returns>Возвращает true, если хэши совпадают, и false в противном случае.</returns>
    bool VerifyPassword(string hashedPassword, string passwordToCheck);
}