using System.Text;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.Services.Files.Services;
using AdvertisementsBoard.Application.AppServices.Services.Passwords.Services;
using AdvertisementsBoard.Infrastructure.DataAccess;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using AdvertisementsBoard.Infrastructure.Mappings.MapConfiguration;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisementsBoard.Infrastructure.ComponentRegistrar;

/// <summary>
///     Регистрация компонентов.
/// </summary>
public static class ComponentReqistrar
{
    /// <summary>
    ///     Регистрация сервисов.
    /// </summary>
    /// <param name="services">IServiceCollection.</param>
    /// <param name="configuration"></param>
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextConfiguration>();

        services.AddDbContext<BaseDbContext>(
            (sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>()
                .Configure((DbContextOptionsBuilder<BaseDbContext>)dbOptions));

        services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BaseDbContext>()));
        services.AddTransient<IFileService, FileService>();
        services.AddScoped<IAdvertisementService, AdvertisementService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICommentService, CommentService>();
        services.AddSingleton<IMapper>(new Mapper(MapperConfigurator.GetMapperConfiguration()));
        services.AddScoped<IPasswordService, PasswordService>();


        #region Настройка аутентификации Jwt

        var secretKey = configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(secretKey))
            throw new ArgumentException(
                "Значение  для ‘Jwt:Key’ не может быть null или состоять только из пробелов.");

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateActor = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });

        #endregion
        
        
        services.AddAuthorization(s =>

            // Политика для незаблокированных пользователей.
            s.AddPolicy("NotBlocked", policy =>
                policy.RequireAssertion(context =>
                    context.User.HasClaim(c =>
                        c.Type == "isBlocked" && bool.Parse(c.Value) == false))));
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
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ISubCategoryRepository, SubCategoryRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
    }
}