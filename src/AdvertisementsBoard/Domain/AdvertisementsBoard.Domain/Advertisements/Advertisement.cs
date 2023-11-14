using AdvertisementsBoard.Domain.Attachments;
using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Comments;
using AdvertisementsBoard.Domain.SubCategories;
using AdvertisementsBoard.Domain.Users;

namespace AdvertisementsBoard.Domain.Advertisements;

/// <summary>
///     Сущность объявления.
/// </summary>
public class Advertisement : BaseEntity
{
    /// <summary>
    ///     Заголовок.
    /// </summary>
    public string Title { get; set; }

    /// <summary>
    ///     Описание.
    /// </summary>
    public string Description { get; set; }

    /// <summary>
    ///     Цена.
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    ///     Наименование тегов.
    /// </summary>
    public string[] TagNames { get; set; }

    /// <summary>
    ///     Статус актиновности.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     Список вложений.
    /// </summary>
    public virtual List<Attachment> Attachments { get; set; }

    /// <summary>
    ///     Идентификатор подкатегории.
    /// </summary>
    public Guid SubCategoryId { get; set; }

    /// <summary>
    ///     Подкатегория.
    /// </summary>
    public virtual SubCategory SubCategory { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Пользователь.
    /// </summary>
    public virtual User User { get; set; }

    /// <summary>
    ///     Комментарии.
    /// </summary>
    public virtual List<Comment> Comments { get; set; }
}