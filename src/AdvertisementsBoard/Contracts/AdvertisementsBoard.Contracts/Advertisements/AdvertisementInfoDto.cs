namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель информации о объявлении.
/// </summary>
public class AdvertisementInfoDto
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

    /*/// <summary>
    ///     Список моделей вложений с информацией.
    /// </summary>
    public List<AttachmentShortInfoDto> Attachments { get; set; }
    
    /// <summary>
    ///     Модель информации о подкатегории.
    /// </summary>
    public SubCategoryShortInfoDto SubCategory { get; set; }

    /// <summary>
    ///     Модель информации о пользователе.
    /// </summary>
    public UserInfoDto User { get; set; }*/
}