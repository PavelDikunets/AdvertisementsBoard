using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// Модель информации об аккаунте.
/// </summary>
public class AccountInfoDto : BaseDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }
}