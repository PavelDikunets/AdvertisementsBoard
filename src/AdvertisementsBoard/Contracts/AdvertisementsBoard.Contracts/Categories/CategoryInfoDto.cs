using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Categories;

/// <summary>
///     Модель информации о категории.
/// </summary>
public class CategoryInfoDto : BaseDto
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; set; }
}