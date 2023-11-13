using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// </summary>
public class AccountBlockDto
{
    /// <summary>
    ///     Признак блокировки.
    /// </summary>
    [Required]
    public bool IsBlocked { get; set; }
}