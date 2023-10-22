using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Domain.Categories;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Categories;

/// <summary>
///     Профиль маппера для категорий.
/// </summary>
public class CategoryProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями Category.
    /// </summary>
    public CategoryProfile()
    {
        CreateMap<Category, CategoryUpdateDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<CategoryUpdateDto, CategoryDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .IgnoreAllNonExisting();

        CreateMap<Category, CategoryShortInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<CategoryDto, CategoryInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<Category, CategoryDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ReverseMap();
    }
}