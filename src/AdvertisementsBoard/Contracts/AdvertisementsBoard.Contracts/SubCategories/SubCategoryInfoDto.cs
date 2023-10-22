using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.SubCategories;

/// <summary>
///     Модель информации о подкатегории.
/// </summary>
public class SubCategoryInfoDto : BaseDto
{
    /// <summary>
    ///     Наименование подкатегории.
    /// </summary>
    public string Name { get; set; }
}