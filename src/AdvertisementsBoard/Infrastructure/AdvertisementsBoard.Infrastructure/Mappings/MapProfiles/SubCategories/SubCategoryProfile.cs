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
    ///     Конструктор, настраивающий маппинг между моделями для подкатегорий.
    /// </summary>
    public SubCategoryProfile()
    {
        CreateMap<SubCategory, SubCategoryDto>().ReverseMap();
        CreateMap<SubCategory, SubCategoryShortInfoDto>();
        CreateMap<SubCategory, SubCategoryUpdatedDto>();

        CreateMap<SubCategoryDto, SubCategoryInfoDto>();
        CreateMap<SubCategoryDto, SubCategoryShortInfoDto>();

        CreateMap<SubCategoryUpdateDto, SubCategoryDto>().IgnoreAllNonExisting();
        CreateMap<SubCategoryCreateDto, SubCategory>().IgnoreAllNonExisting();
    }
}