using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.SubCategories;

/// <summary>
///     Профиль маппера для подкатегорий.
/// </summary>
public class SubCategoryProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями SubCategoryDtos и SubCategory.
    /// </summary>
    public SubCategoryProfile()
    {
        CreateMap<SubCategory, SubCategoryUpdateDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<SubCategoryUpdateDto, SubCategoryDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .IgnoreAllNonExisting();

        CreateMap<SubCategory, SubCategoryShortInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<SubCategoryDto, SubCategoryInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<SubCategory, SubCategoryDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ReverseMap();
    }
}