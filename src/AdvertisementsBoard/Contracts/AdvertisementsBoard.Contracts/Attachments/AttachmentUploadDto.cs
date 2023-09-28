using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель загрузки вложения.
/// </summary>
public class AttachmentUploadDto
{
    /// <summary>
    ///     Форма загрузки файла.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
}