using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
/// Модель создания пользователя.
/// </summary>
public class UserCreateDto
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(64, MinimumLength = 3, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }
    
    /// <summary>
    /// Элетронная почта.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [EmailAddress(ErrorMessage = "Недействительный адрес электронной почты.")]
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Password { get; set; }
    
    /// <summary>
    /// Подтверждение пароля.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 6, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string ConfirmPassword { get; set; }
}