using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.SubCategories;

/// <summary>
///     Модель создания подкатегории.
/// </summary>
public class SubCategoryCreateDto
{
    /// <summary>
    ///     Наименование подкатегории.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }

    /// <summary>
    ///     Идентификатор категории.
    /// </summary>
    [Required]
    public Guid CategoryId { get; set; }
}