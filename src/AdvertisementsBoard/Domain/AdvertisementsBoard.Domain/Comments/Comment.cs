using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Users;

namespace AdvertisementsBoard.Domain.Comments;

/// <summary>
///     Сущность комментария.
/// </summary>
public class Comment : BaseEntity
{
    /// <summary>
    ///     Текст комментария.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Дата создания комментария.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    public virtual Advertisement Advertisement { get; set; }

    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }
}