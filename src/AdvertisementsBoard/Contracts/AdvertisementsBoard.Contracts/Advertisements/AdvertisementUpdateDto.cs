using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель редактирования объявления.
/// </summary>
public class AdvertisementUpdateDto : AdvertisementCreateDto
{
    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    [Required]
    public Guid Id { get; set; }
}