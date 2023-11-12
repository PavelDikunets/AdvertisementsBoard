using AdvertisementsBoard.Contracts.Users;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
///     Модель созданного аккаунта.
/// </summary>
public class AccountCreatedDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public UserCreatedDto User { get; set; }
}