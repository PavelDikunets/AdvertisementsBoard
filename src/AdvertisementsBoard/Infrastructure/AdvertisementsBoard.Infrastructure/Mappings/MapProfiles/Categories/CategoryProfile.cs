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
    ///     Конструктор, настраивающий маппинг между моделями для категорий.
    /// </summary>
    public CategoryProfile()
    {
        CreateMap<Category, CategoryDto>().ReverseMap();
        CreateMap<Category, CategoryShortInfoDto>();
        CreateMap<Category, CategoryUpdatedDto>();

        CreateMap<CategoryDto, CategoryInfoDto>();
        CreateMap<CategoryDto, CategoryShortInfoDto>();

        CreateMap<CategoryUpdateDto, CategoryDto>().IgnoreAllNonExisting();
        CreateMap<CategoryCreateDto, Category>().IgnoreAllNonExisting();
    }
}