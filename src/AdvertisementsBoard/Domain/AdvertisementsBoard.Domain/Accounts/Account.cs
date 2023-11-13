using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Users;

namespace AdvertisementsBoard.Domain.Accounts;

/// <summary>
///     Сущность аккаунта.
/// </summary>
public class Account : BaseEntity
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
    ///     Сущность пользователя.
    /// </summary>
    public virtual User User { get; set; }
}