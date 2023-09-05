using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Объявление.
/// </summary>
public class AdvertisementDto : BaseDto
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
    ///     Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Наименование категории
    /// </summary>
    public string CategoryName { get; set; }
}