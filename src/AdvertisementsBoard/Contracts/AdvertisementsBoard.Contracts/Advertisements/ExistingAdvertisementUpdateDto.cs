using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Advertisements;

/// <summary>
///     Модель обновления существующего объявления.
/// </summary>
public class ExistingAdvertisementUpdateDto : AdvertisementCreateDto
{
   /// <summary>
   ///     Идентификатор существующего объявления.
   /// </summary>
   [Required]
    public Guid Id { get; set; }
}