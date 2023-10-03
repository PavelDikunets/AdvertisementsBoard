using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;

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
    ///     Список объявлений.
    /// </summary>
    public virtual List<Advertisement> Advertisements { get; set; }
}