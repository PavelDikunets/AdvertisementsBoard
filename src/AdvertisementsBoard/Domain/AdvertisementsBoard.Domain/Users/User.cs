using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;

namespace AdvertisementsBoard.Domain.Users;

/// <summary>
///     Сущность пользователя.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Адрес электронной почты.
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     Хэш пароля.
    /// </summary>
    public string PasswordHash { get; set; }

    /// <summary>
    ///     Список объявлений.
    /// </summary>
    public virtual List<Advertisement> Advertisements { get; set; }
}