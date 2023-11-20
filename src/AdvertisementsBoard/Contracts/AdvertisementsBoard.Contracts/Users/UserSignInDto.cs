using AdvertisementsBoard.Common.Enums.Users;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель с клэймами пользователя.
/// </summary>
public class UserSignInDto
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