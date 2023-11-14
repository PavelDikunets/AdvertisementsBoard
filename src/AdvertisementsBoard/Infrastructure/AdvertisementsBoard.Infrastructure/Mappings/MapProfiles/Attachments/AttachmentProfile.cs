using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Attachments;

/// <summary>
///     Профиль маппера для вложений.
/// </summary>
public class AttachmentProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями для вложений.
    /// </summary>
    public AttachmentProfile()
    {
        CreateMap<AttachmentUploadDto, Attachment>().IgnoreAllNonExisting();
        CreateMap<AttachmentEditDto, Attachment>().IgnoreAllNonExisting();

        CreateMap<Attachment, AttachmentShortInfoDto>();
        CreateMap<Attachment, AttachmentInfoDto>();
    }
}