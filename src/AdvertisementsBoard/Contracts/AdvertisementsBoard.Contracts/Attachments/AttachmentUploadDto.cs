using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель загрузки вложения.
/// </summary>
public class AttachmentUploadDto
{
    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    [Required]
    public Guid AdvertisementId { get; set; }

    /// <summary>
    ///     Форма загрузки файла.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
}