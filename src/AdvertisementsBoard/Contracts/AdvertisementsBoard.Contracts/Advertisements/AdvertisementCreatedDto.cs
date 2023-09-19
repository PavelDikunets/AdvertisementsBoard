namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
/// Модель созданного объявления.
/// </summary>
public class AdvertisementCreatedDto
{
    /// <summary>
    /// Идентификатор созданного объявления.
    /// </summary>
    public Guid Id { get; set; }
}