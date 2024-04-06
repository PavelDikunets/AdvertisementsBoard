namespace AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;

/// <summary>
///     Исключение, когда происходит ошибка при аутентификации.
/// </summary>
public class AuthenticationFailedException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса
    ///     <see cref="AuthenticationFailedException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    public AuthenticationFailedException() : base("Ошибка аутентификации.")
    {
    }
}