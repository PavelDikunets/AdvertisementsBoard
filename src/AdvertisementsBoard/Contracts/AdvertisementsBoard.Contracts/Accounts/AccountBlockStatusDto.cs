namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
///     Модель блокировки аккаунта.
/// </summary>
public class AccountBlockStatusDto
{
    /// <summary>
    ///     Признак блокировки.
    /// </summary>
    public bool IsBlocked { get; set; }
}