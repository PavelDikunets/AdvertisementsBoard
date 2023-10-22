namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда пароли не совпадают.
/// </summary>
public class PasswordException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PasswordException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public PasswordException(string message) : base(message)
    {
    }
}