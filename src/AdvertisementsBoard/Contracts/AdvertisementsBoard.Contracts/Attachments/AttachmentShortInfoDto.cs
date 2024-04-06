using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель с краткой информацией о вложении.
/// </summary>
public class AttachmentShortInfoDto : BaseDto
{
    /// <summary>
    ///     Url файла.
    /// </summary>
    public string Url { get; set; }
}