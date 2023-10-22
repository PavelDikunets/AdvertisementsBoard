using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель информации о пользователе.
/// </summary>
public class UserShortInfoDto : BaseDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }
}