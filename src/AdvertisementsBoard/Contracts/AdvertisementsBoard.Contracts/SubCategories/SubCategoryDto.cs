using AdvertisementsBoard.Contracts.Base;

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
}