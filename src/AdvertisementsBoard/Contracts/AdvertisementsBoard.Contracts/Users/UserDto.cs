using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель пользователя.
/// </summary>
public class UserDto : BaseDto
{
    /// <summary>
    ///     Никнейм.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Имя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Наименование роли
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    ///     Идентификатор аккаунта.
    /// </summary>
    public Guid AccountId { get; set; }
}