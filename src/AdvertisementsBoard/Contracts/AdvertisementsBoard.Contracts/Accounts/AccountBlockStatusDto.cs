using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
///     Модель блокировки аккаунта.
/// </summary>
public class AccountBlockStatusDto : BaseDto
{
    /// <summary>
    ///     Признак блокировки.
    /// </summary>

    public bool IsBlocked { get; set; }
}