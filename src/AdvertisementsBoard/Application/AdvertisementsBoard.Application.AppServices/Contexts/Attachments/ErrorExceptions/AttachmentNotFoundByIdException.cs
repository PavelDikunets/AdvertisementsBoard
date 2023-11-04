namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.ErrorExceptions;

/// <summary>
///     Исключение, когда вложение не найдено по идентификатору.
/// </summary>
public class AttachmentNotFoundByIdException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AttachmentNotFoundByIdException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="attachmentId">Идентификатор вложения.</param>
    public AttachmentNotFoundByIdException(Guid attachmentId) : base(
        $"Вложение с идентификатором '{attachmentId}' не найдено.")
    {
    }
}