using AdvertisementsBoard.Contracts.Base;
using AdvertisementsBoard.Contracts.Users;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// </summary>
public class AccountDto : BaseDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    ///     Дата регистрации.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Статус блокировки.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public UserDto User { get; set; }
}