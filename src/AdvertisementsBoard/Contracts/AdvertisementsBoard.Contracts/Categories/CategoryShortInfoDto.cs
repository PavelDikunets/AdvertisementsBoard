using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Categories;

/// <summary>
///     Модель категории с краткой информацией.
/// </summary>
public class CategoryShortInfoDto : BaseDto
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; set; }
}