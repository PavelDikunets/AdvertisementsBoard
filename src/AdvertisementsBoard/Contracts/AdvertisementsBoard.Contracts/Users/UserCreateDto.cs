using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель создания пользователя.
/// </summary>
public class UserCreateDto
{
    /// <summary>
    ///     Никнэйм.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string NickName { get; set; }
}