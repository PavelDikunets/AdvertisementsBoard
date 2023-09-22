using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Модель обновления существующего вложения.
/// </summary>
public class ExistingAttachmentUpdateDto
{
    /// <summary>
    ///     Идентификатор вложения.
    /// </summary>
    [Required]
    public Guid Id { get; set; }

    public string FileName { get; set; }

    /// <summary>
    ///     Форма загрузки файла.
    /// </summary>
    [Required]
    public IFormFile File { get; set; }
}