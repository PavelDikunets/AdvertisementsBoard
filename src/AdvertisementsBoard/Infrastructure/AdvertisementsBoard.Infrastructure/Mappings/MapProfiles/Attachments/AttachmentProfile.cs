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
        CreateMap<Attachment, AttachmentDto>().ReverseMap();
        CreateMap<Attachment, AttachmentShortInfoDto>();
        CreateMap<Attachment, AttachmentUpdatedDto>();

        CreateMap<AttachmentDto, AttachmentInfoDto>();
        CreateMap<AttachmentDto, AttachmentShortInfoDto>();

        CreateMap<AttachmentUploadDto, AttachmentDto>().IgnoreAllNonExisting();
        CreateMap<AttachmentUpdateDto, AttachmentDto>().IgnoreAllNonExisting();
    }
}