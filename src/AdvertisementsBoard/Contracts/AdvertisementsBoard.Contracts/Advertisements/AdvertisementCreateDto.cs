using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель создания объявления.
/// </summary>
public class AdvertisementCreateDto
{
    /// <summary>
    ///     Заголовок.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 5, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Title { get; set; }

    /// <summary>
    ///     Описание.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(500, MinimumLength = 20, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Description { get; set; }

    /// <summary>
    ///     Цена.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [Range(0, 100_000_000_000, ErrorMessage = "Поле {0} должно быть в диапазоне от {1} до {2}.")]
    public decimal Price { get; set; }

    /// <summary>
    ///     Наименование тегов.
    /// </summary>
    [Required]
    [MaxLength(6, ErrorMessage = "Максимальное количество {0}: {1}")]
    [MinLength(1, ErrorMessage = "Минимальное количество {0}: {1}")]
    public string[] TagNames { get; set; }

    /// <summary>
    ///     Статус активности.
    /// </summary>
    [Required]
    public bool IsActive { get; set; }

    /// <summary>
    ///     Идентификатор подкатегории.
    /// </summary>
    [Required]
    public Guid SubCategoryId { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    [Required]
    public Guid UserId { get; set; }
}