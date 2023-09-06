using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель обновления вложения.
/// </summary>
public class UpdateAttachmentDto : BaseDto
{
    /// <summary>
    ///     Путь к файлу.
    /// </summary>
    public string FilePath { get; set; }
}