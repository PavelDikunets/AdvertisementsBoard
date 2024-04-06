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
        CreateMap<Advertisement, AdvertisementUpdatedDto>();
        CreateMap<Advertisement, AdvertisementInfoDto>();
        CreateMap<Advertisement, AdvertisementShortInfoDto>()
            .ForMember(dest => dest.Attachment, map => map.MapFrom(src => src.Attachments.FirstOrDefault()));

        CreateMap<AdvertisementUpdateDto, Advertisement>().IgnoreAllNonExisting();
        CreateMap<AdvertisementCreateDto, Advertisement>().IgnoreAllNonExisting();
    }
}