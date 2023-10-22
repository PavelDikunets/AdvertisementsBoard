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
        CreateMap<Advertisement, AdvertisementUpdatedDto>()
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.TagNames, map => map.MapFrom(src => src.TagNames))
            .ForMember(dest => dest.IsActive, map => map.MapFrom(src => src.IsActive));

        CreateMap<AdvertisementUpdateDto, AdvertisementDto>()
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.TagNames, map => map.MapFrom(src => src.TagNames))
            .ForMember(dest => dest.IsActive, map => map.MapFrom(src => src.IsActive))
            .ForPath(dest => dest.User.Id, map => map.MapFrom(src => src.UserId))
            .IgnoreAllNonExisting();

        CreateMap<AdvertisementCreateDto, Advertisement>()
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.TagNames, map => map.MapFrom(src => src.TagNames))
            .ForMember(dest => dest.IsActive, map => map.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.SubCategoryId, map => map.MapFrom(src => src.SubCategoryId))
            .ForMember(dest => dest.UserId, map => map.MapFrom(src => src.UserId))
            .IgnoreAllNonExisting();

        CreateMap<Advertisement, AdvertisementShortInfoDto>()
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.Attachment, map => map.MapFrom(src => src.Attachments.FirstOrDefault()));

        CreateMap<AdvertisementDto, AdvertisementInfoDto>()
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.TagNames, map => map.MapFrom(src => src.TagNames))
            .ForMember(dest => dest.IsActive, map => map.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Attachments, map => map.MapFrom(src => src.Attachments))
            .ForMember(dest => dest.Category, map => map.MapFrom(src => src.Category))
            .ForMember(dest => dest.SubCategory, map => map.MapFrom(src => src.SubCategory))
            .ForMember(dest => dest.User, map => map.MapFrom(src => src.User));

        CreateMap<Advertisement, AdvertisementDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Title, map => map.MapFrom(src => src.Title))
            .ForMember(dest => dest.Description, map => map.MapFrom(src => src.Description))
            .ForMember(dest => dest.Price, map => map.MapFrom(src => src.Price))
            .ForMember(dest => dest.TagNames, map => map.MapFrom(src => src.TagNames))
            .ForMember(dest => dest.IsActive, map => map.MapFrom(src => src.IsActive))
            .ForMember(dest => dest.Attachments, map => map.MapFrom(src => src.Attachments))
            .ForMember(dest => dest.SubCategory, map => map.MapFrom(src => src.SubCategory))
            .ForMember(dest => dest.Category, map => map.MapFrom(src => src.SubCategory.Category))
            .ForMember(dest => dest.User, map => map.MapFrom(src => src.User))
            .ReverseMap();
    }
}