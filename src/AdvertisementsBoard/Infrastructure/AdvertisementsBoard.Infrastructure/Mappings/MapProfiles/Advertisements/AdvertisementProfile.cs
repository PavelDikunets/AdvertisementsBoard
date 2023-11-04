using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Advertisements;

/// <summary>
///     Профиль маппера для объявлений.
/// </summary>
public class AdvertisementProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями AdvertisementDtos и Advertisement.
    /// </summary>
    public AdvertisementProfile()
    {
        CreateMap<Advertisement, AdvertisementDto>()
            .ForMember(dest => dest.Category, map => map.MapFrom(src => src.SubCategory.Category));

        CreateMap<Advertisement, AdvertisementShortInfoDto>()
            .ForMember(dest => dest.Attachment, map => map.MapFrom(src => src.Attachments.FirstOrDefault()));

        CreateMap<Advertisement, AdvertisementUpdatedDto>();

        CreateMap<AdvertisementDto, Advertisement>();
        CreateMap<AdvertisementDto, AdvertisementInfoDto>();
        CreateMap<AdvertisementDto, AdvertisementShortInfoDto>()
            .ForMember(dest => dest.Attachment, map => map.MapFrom(src => src.Attachments.FirstOrDefault()));

        CreateMap<AdvertisementCreateDto, Advertisement>().IgnoreAllNonExisting();
        CreateMap<AdvertisementUpdateDto, AdvertisementDto>().IgnoreAllNonExisting();
    }
}