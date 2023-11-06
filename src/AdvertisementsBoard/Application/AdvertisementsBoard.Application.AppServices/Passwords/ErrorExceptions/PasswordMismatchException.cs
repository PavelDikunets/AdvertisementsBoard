namespace AdvertisementsBoard.Application.AppServices.Passwords.ErrorExceptions;

/// <summary>
///     Исключение, когда пароли не совпадают.
/// </summary>
public class PasswordMismatchException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PasswordMismatchException" /> с указанием сообщения об ошибке.
    /// </summary>
    public PasswordMismatchException() : base("Пароли не совпадают.")
    {
    }
}