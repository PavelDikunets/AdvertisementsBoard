using AdvertisementsBoard.Domain.Attachments;
using AdvertisementsBoard.Domain.Base;
using AdvertisementsBoard.Domain.Categories;

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
    ///     Идентификатор категории.
    /// </summary>
    public Guid CategoryId { get; set; }

    /// <summary>
    ///     Категория объявления.
    /// </summary>
    public virtual Category Category { get; set; }

    /// <summary>
    ///     Список вложений.
    /// </summary>
    public virtual List<Attachment> Attachments { get; set; }
}