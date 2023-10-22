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
    ///     Конструктор, настраивающий маппинг между моделями AttachmentDtos и Attachment.
    /// </summary>
    public AttachmentProfile()
    {
        CreateMap<AttachmentUpdateDto, AttachmentDto>()
            .IgnoreAllNonExisting();

        CreateMap<Attachment, AttachmentShortInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Url, map => map.MapFrom(src => src.Url));

        CreateMap<AttachmentDto, AttachmentInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Url, map => map.MapFrom(src => src.Url));

        CreateMap<Attachment, AttachmentDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Url, map => map.MapFrom(src => src.Url))
            .ReverseMap();
    }
}