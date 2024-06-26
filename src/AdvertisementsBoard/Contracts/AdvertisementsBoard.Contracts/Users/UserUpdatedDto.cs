namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель c обновленной информацией о пользователе.
/// </summary>
public class UserUpdatedDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; }
}