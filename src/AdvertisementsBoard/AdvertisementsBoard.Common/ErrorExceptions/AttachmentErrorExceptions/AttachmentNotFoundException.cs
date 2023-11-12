namespace AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;

/// <summary>
///     Исключение, когда вложение не найдено по идентификатору.
/// </summary>
public class AttachmentNotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AttachmentNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    public AttachmentNotFoundException() : base("Вложение не найдено.")
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AttachmentNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="attachmentId">Идентификатор вложения.</param>
    public AttachmentNotFoundException(Guid attachmentId) : base(
        $"Вложение с идентификатором '{attachmentId}' не найдено.")
    {
    }
}