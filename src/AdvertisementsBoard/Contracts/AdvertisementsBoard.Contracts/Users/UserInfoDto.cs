namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель информации о пользователе.
/// </summary>
public class UserInfoDto
{
    /// <summary>
    ///     Никнейм.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; }
}