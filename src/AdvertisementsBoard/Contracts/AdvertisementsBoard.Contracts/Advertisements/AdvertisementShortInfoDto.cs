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
    ///     Наименование категории
    /// </summary>
    public string CategoryName { get; set; }
}