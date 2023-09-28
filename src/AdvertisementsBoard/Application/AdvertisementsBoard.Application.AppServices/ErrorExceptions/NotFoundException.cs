namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда требуемый ресурс не найден.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="NotFoundException" /> без указания сообщения об ошибке.
    /// </summary>
    public NotFoundException()
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="NotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public NotFoundException(string message) : base(message)
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="NotFoundException" /> с указанием сообщения об ошибке и ссылкой на
    ///     внутреннее исключение, вызвавшее данное исключение.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    /// <param name="innerException">Исключение, вызвавшее текущее исключение, или null, если внутреннее исключение не указано.</param>
    public NotFoundException(string message, Exception innerException) : base(message, innerException)
    {
    }
}