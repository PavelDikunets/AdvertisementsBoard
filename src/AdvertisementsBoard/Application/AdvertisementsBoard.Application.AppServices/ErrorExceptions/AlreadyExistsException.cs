namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда ресурс уже создан.
/// </summary>
public class AlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AlreadyExistsException" /> без указания сообщения об ошибке.
    /// </summary>
    public AlreadyExistsException()
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AlreadyExistsException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public AlreadyExistsException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AlreadyExistsException" /> с указанием сообщения об ошибке и ссылкой на
    ///     внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, вызвавшее текущее исключение, или null, если внутреннее исключение не указано.</param>
    public AlreadyExistsException(string message, Exception innerException) : base(message, innerException)
    {
    }
}