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
        CreateMap<CategoryCreateDto, Category>().IgnoreAllNonExisting();

        CreateMap<Category, CategoryShortInfoDto>();
        CreateMap<Category, CategoryInfoDto>();
    }
}