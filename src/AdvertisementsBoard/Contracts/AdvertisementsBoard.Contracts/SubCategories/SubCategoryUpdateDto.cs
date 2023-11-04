using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.SubCategories;

/// <summary>
///     Модель обновления подкатегории.
/// </summary>
public class SubCategoryUpdateDto
{
    /// <summary>
    ///     Наименование подкатегории.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }
}