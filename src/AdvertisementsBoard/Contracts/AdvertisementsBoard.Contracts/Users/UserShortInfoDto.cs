using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель с краткой информацией о пользователе.
/// </summary>
public class UserShortInfoDto : BaseDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string NickName { get; set; }
}