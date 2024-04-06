namespace AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;

/// <summary>
///     Исключение, когда учетные данные для входа в аккаунт неверны.
/// </summary>
public class InvalidSignInCredentialsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="InvalidSignInCredentialsException" /> с указанием сообщения об ошибке.
    /// </summary>
    public InvalidSignInCredentialsException() : base("Логин и/или пароль неверные.")
    {
    }
}