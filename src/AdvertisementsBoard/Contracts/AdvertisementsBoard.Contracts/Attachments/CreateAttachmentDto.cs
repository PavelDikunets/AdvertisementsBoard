namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель создания вложения.
/// </summary>
public class CreateAttachmentDto
{
    /// <summary>
    ///     Путь к файлу.
    /// </summary>
    public string FilePath { get; set; }
}