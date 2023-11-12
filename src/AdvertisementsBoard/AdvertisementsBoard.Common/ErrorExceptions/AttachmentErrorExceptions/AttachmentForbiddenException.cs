namespace AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;

/// <summary>
///     Исключение, когда доступ для вложений запрещен.
/// </summary>
public class AttachmentForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AttachmentForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    public AttachmentForbiddenException() : base("Нет прав на доступ к вложениям.")
    {
    }
}