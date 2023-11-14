namespace AdvertisementsBoard.Contracts.Accounts;

/// <summary>
///     Модель аккаунта для просмотра администратором.
/// </summary>
public class AccountAdminDto
{
    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public bool IsBlocked { get; set; }

    /// <summary>
    ///     Дата регистрации.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }
}