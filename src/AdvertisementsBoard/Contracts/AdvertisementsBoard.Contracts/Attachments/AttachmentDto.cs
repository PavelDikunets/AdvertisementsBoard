using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Attachments;

/// <summary>
///     Вложения.
/// </summary>
public class AttachmentDto : BaseDto
{
    /// <summary>
    ///     Путь к файлу.
    /// </summary>
    public string FilePath { get; set; }
}