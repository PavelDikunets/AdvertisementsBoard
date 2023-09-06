using AdvertisementsBoard.Domain.Base;

namespace AdvertisementsBoard.Domain.Attachments;

public class Attachment : BaseEntity
{
    /// <summary>
    ///     Путь к файлу.
    /// </summary>
    public string FilePath { get; set; }
}