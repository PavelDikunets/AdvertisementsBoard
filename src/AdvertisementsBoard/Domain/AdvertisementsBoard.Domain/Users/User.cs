using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Domain.Accounts;
using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Comments;

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
    ///     Никнейм.
    /// </summary>
    public string NickName { get; set; }

    /// <summary>
    ///     Комментарии.
    /// </summary>
    public virtual List<Comment> Comments { get; set; }
}