using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Base;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Contracts.Users;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель объявления.
/// </summary>
public class AdvertisementDto : BaseDto
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
    ///     Статус активности.
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    ///     Список моделей вложений.
    /// </summary>
    public List<AttachmentDto> Attachments { get; set; }

    /// <summary>
    ///     Модель подкатегории.
    /// </summary>
    public SubCategoryDto SubCategory { get; set; }

    /// <summary>
    ///     Модель категории.
    /// </summary>
    public CategoryDto Category { get; set; }

    /// <summary>
    ///     Модель пользователя.
    /// </summary>
    public UserDto User { get; set; }


    public List<CommentDto> Comments { get; set; }

    /// <summary>
    /// </summary>
    public Guid SubCategoryId { get; set; }

    /// <summary>
    /// </summary>
    public Guid UserId { get; set; }
}