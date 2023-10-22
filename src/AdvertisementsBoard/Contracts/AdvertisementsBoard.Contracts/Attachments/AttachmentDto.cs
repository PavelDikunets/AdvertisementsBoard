using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель вложения.
/// </summary>
public class AttachmentDto : BaseDto
{
    /// <summary>
    ///     Url файла.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }
}