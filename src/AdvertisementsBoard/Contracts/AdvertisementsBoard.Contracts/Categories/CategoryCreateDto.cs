using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Categories;

/// <summary>
///     Модель создания категории.
/// </summary>
public class CategoryCreateDto
{
    /// <summary>
    ///     Наименование категории.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }
}