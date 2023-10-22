using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель информации о пользователе.
/// </summary>
public class UserInfoDto : BaseDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }
}