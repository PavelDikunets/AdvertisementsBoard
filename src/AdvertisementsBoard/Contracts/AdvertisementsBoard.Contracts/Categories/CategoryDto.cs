using AdvertisementsBoard.Contracts.Base;

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
}