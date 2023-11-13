using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель обновления вложения.
/// </summary>
public class AttachmentEditDto
{
    /// <summary>
    ///     Форма загрузки файла.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
}