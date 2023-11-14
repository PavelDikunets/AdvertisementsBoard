namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
/// </summary>
public class AccountInfoDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Дата регистрации.
    /// </summary>
    public DateTime Created { get; set; }
}