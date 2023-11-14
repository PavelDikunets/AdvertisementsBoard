using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель пользователя.
/// </summary>
public class UserDto : BaseDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Никнейм.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Роль пользователя.
    /// </summary>
    public UserRole Role { get; set; }
}