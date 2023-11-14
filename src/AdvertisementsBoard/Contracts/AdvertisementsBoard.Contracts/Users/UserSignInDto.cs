using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель с клэймами пользователя.
/// </summary>
public class UserSignInDto : BaseDto
{
    /// <summary>
    ///     Никнейм.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Роль пользователя.
    /// </summary>
    public UserRole Role { get; set; }
}