namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда пароли не совпадают.
/// </summary>
public class PasswordMismatchException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PasswordMismatchException" /> без указания сообщения об ошибке.
    /// </summary>
    public PasswordMismatchException()
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PasswordMismatchException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public PasswordMismatchException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PasswordMismatchException" /> с указанием сообщения об ошибке и ссылкой на
    ///     внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, вызвавшее текущее исключение, или null, если внутреннее исключение не указано.</param>
    public PasswordMismatchException(string message, Exception innerException) : base(message, innerException)
    {
    }
}