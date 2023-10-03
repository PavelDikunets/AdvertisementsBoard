using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель информации о объявлении.
/// </summary>
public class AdvertisementInfoDto
{
    /// <summary>
    ///     Заголовок.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Описание.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Цена.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Наименование тегов.
    /// </summary>
    public string[] TagNames { get; set; }

    /// <summary>
    ///     Статус активности.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     Коллекция вложений.
    /// </summary>
    public List<AttachmentInfoDto> Attachments { get; set; }

    /// <summary>
    ///     Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string CategoryName { get; set; }
}