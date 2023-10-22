namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда требуемый ресурс не найден.
/// </summary>
public class NotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="NotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public NotFoundException(string message) : base(message)
    {
    }
}