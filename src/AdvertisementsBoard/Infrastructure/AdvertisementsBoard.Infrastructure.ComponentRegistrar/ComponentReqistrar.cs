using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementsBoard.Infrastructure.ComponentRegistrar;

/// <summary>
///     Регистрация компонентов.
/// </summary>
public static class ComponentReqistrar
{
    /// <summary>
    ///     Регистрация сервисов.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IAdvertisementService, AdvertisementService>();
    }

    /// <summary>
    ///     Регистрация репозиториев.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseDbRepository<>), typeof(BaseDbRepository<>));
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
    }
}