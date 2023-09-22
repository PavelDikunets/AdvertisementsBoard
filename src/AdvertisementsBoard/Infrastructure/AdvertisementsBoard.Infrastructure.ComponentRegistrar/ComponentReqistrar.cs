using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Files.Services;
using AdvertisementsBoard.Infrastructure.DataAccess;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
        services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextConfiguration>();

        services.AddDbContext<BaseDbContext>(
            (sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>()
                .Configure((DbContextOptionsBuilder<BaseDbContext>)dbOptions));

        services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BaseDbContext>()));


        services.AddTransient<IFileService, FileService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
    }

    /// <summary>
    ///     Регистрация репозиториев.
    /// </summary>
    /// <param name="services">Сервисы.</param>
    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IBaseDbRepository<>), typeof(BaseDbRepository<>));
        services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
        services.AddScoped<IAttachmentRepository, AttachmentRepository>();
    }
}