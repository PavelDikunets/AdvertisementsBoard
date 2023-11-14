using AdvertisementsBoard.Contracts.Base;
using AdvertisementsBoard.Contracts.Categories;

namespace AdvertisementsBoard.Contracts.SubCategories;

/// <summary>
///     Модель подкатегории.
/// </summary>
public class SubCategoryDto : BaseDto
{
    /// <summary>
    ///     Наименование подкатегории.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Категория.
    /// </summary>
    public CategoryDto Category { get; set; }
}