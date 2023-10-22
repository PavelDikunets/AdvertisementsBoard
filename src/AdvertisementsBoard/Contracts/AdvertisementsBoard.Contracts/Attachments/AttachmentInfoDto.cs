using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель вложения.
/// </summary>
public class AttachmentInfoDto : BaseDto
{
    /// <summary>
    ///     Url файла.
    /// </summary>
    public string Url { get; set; }
}