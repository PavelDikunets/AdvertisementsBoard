using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// Модель аккаунта с краткой информацией.
/// </summary>
public class AccountShortInfoDto : BaseDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Статус блокировки.
    /// </summary>
    public bool IsBlocked { get; set; }
}