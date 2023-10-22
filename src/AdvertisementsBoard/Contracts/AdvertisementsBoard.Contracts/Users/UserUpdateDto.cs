using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель обновления пользователя.
/// </summary>
public class UserUpdateDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }

    /// <summary>
    ///     Текущий пароль.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string CurrentPassword { get; set; }

    /// <summary>
    ///     Новый пароль.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string NewPassword { get; set; }

    /// <summary>
    ///     Подтверждение нового пароля.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string ConfirmPassword { get; set; }
}