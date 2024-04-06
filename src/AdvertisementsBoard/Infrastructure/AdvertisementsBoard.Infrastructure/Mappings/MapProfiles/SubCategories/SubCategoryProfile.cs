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
        CreateMap<SubCategory, SubCategoryInfoDto>();
        CreateMap<SubCategory, SubCategoryShortInfoDto>();

        CreateMap<SubCategoryCreateDto, SubCategory>().IgnoreAllNonExisting();
    }
}