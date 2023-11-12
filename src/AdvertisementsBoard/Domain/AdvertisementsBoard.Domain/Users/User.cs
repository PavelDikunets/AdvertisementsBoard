using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Domain.Accounts;
using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;

namespace AdvertisementsBoard.Domain.Users;

/// <summary>
///     Сущность пользователя.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    ///     Никнейм пользователя.
    /// </summary>
    /// public string NickName { get; set; }
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///     Номер телефона.
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     Перечисление ролей пользователя.
    /// </summary>
    public UserRole Role { get; set; }

    /// <summary>
    ///     Список объявлений.
    /// </summary>
    public virtual List<Advertisement> Advertisements { get; set; }

    /// <summary>
    ///     Аккаунт пользователя.
    /// </summary>
    public virtual Account Account { get; set; }

    /// <summary>
    /// </summary>
    public Guid AccountId { get; set; }

    /// <summary>
    /// </summary>
    public string NickName { get; set; }
}