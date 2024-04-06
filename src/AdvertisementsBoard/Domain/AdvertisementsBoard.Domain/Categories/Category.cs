using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.SubCategories;

namespace AdvertisementsBoard.Domain.Categories;

/// <summary>
///     Сущность категории.
/// </summary>
public class Category : BaseEntity
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Список подкатегорий.
    /// </summary>
    public virtual List<SubCategory> SubCategories { get; set; }
}