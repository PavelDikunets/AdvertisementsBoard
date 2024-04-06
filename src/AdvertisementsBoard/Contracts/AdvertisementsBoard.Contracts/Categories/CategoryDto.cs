using AdvertisementsBoard.Contracts.Base;
using AdvertisementsBoard.Contracts.SubCategories;

namespace AdvertisementsBoard.Contracts.Categories;

/// <summary>
///     Модель категории.
/// </summary>
public class CategoryDto : BaseDto
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Список подкатегорий.
    /// </summary>
    public List<SubCategoryDto> SubCategories { get; set; }
}