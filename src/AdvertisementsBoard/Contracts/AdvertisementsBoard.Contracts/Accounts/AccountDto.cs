using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// </summary>
public class AccountDto : BaseDto
{
    /// <summary>
    ///     Статус блокировки.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    ///     Захешированный пароль.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}