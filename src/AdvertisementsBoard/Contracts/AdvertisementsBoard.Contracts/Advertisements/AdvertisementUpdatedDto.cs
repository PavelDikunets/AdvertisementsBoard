namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Обновленная модель объявления.
/// </summary>
public class AdvertisementUpdatedDto
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
}