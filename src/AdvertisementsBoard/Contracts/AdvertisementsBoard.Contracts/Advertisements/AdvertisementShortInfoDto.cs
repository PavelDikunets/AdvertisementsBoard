using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель объявления с краткой информацией.
/// </summary>
public class AdvertisementShortInfoDto : BaseDto
{
    /// <summary>
    ///     Заголовок.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Цена.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Модель с краткой информацией о вложении.
    /// </summary>
    public AttachmentShortInfoDto Attachment { get; set; }
}