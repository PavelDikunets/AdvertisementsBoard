using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Accounts;
using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Advertisements;
using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Attachments;
using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Categories;
using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.SubCategories;
using AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Users;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapConfiguration;

/// <summary>
///     Конфигурации маппера для настройки профилей маппинга.
/// </summary>
public static class MapperConfigurator
{
    /// <summary>
    ///     Создает и возвращает конфигурацию маппера с добавленными профилями.
    /// </summary>
    /// <returns>Конфигурация маппера с добавленными профилями.</returns>
    public static MapperConfiguration GetMapperConfiguration()
    {
        var configuration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<AdvertisementProfile>();
            cfg.AddProfile<AttachmentProfile>();
            cfg.AddProfile<CategoryProfile>();
            cfg.AddProfile<SubCategoryProfile>();
            cfg.AddProfile<UserProfile>();
            cfg.AddProfile<AccountProfile>();
        });
        configuration.AssertConfigurationIsValid();
        return configuration;
    }
}