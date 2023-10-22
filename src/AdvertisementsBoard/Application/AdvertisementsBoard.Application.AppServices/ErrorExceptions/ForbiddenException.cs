namespace AdvertisementsBoard.Application.AppServices.ErrorExceptions;

/// <summary>
///     Исключение, когда к ресурсу доступ запрещен.
/// </summary>
public class ForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="ForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="message">Сообщение, описывающее ошибку.</param>
    public ForbiddenException(string message) : base(message)
    {
    }
}