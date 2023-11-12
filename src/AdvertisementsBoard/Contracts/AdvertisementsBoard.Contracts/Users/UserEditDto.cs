using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель обновления пользователя.
/// </summary>
public class UserEditDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    [StringLength(32, MinimumLength = 2, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Name { get; set; }

    /// <summary>
    ///     Номер телефона.
    /// </summary>
    [RegularExpression(@"^\+\d \(\d{3}\)\d{3}-\d{2}-\d{2}$")]
    public string PhoneNumber { get; set; }
}