using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.SubCategories;

/// <summary>
///     Модель информации о подкатегории.
/// </summary>
public class SubCategoryShortInfoDto : BaseDto
{
    /// <summary>
    ///     Наименование подкатегории.
    /// </summary>
    public string Name { get; set; }

}