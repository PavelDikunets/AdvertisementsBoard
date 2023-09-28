using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;

namespace AdvertisementsBoard.Domain.Attachments;

public class Attachment : BaseEntity
{
    /// <summary>
    ///     Путь к файлу.
    /// </summary>
    public string Url { get; set; }

    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    ///     Объявление.
    /// </summary>
    public virtual Advertisement Advertisement { get; set; }
}