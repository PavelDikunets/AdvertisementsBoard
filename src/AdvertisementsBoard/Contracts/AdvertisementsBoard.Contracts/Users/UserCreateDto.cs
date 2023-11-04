using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель создания пользователя.
/// </summary>
public class UserCreateDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }

    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [EmailAddress(ErrorMessage = "Недействительный адрес электронной почты.")]
    [StringLength(32, ErrorMessage = "Длина {0} не должна превышать {1} символов.")]
    [RegularExpression(@"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$",
        ErrorMessage = "Неверный формат электронной почты.")]
    public string Email { get; set; }

    /// <summary>
    ///     Пароль.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Password { get; set; }

    /// <summary>
    ///     Подтверждение пароля.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string ConfirmPassword { get; set; }
}