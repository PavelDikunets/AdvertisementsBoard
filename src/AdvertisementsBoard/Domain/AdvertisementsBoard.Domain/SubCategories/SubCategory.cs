using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Categories;

namespace AdvertisementsBoard.Domain.SubCategories;

/// <summary>
///     Сущность подкатегории.
/// </summary>
public class SubCategory : BaseEntity
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
    public virtual Category Category { get; set; }

    /// <summary>
    ///     Список объявлений.
    /// </summary>
    public virtual List<Advertisement> Advertisements { get; set; }
}