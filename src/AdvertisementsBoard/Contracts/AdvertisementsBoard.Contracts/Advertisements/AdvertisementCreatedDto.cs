using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель создания объявления.
/// </summary>
public class AdvertisementCreatedDto : BaseDto
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
    ///     Идентификатор подкатегории.
    /// </summary>
    public Guid SubCategoryId { get; set; }
}