using AdvertisementsBoard.Contracts.SubCategories;

namespace AdvertisementsBoard.Contracts.Categories;

/// <summary>
///     Модель информации о категории.
/// </summary>
public class CategoryInfoDto
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Список подкатегорий.
    /// </summary>
    public List<SubCategoryShortInfoDto> SubCategories { get; set; }
}