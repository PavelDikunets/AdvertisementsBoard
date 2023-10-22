using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель пользователя.
/// </summary>
public class UserDto : BaseDto
{
    /// <summary>
    ///     Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; }
}