using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Hosts.Api.Controllers;
using AdvertisementsBoard.Hosts.Api.Middlewares;
using AdvertisementsBoard.Infrastructure.ComponentRegistrar;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddServices(builder.Configuration);

builder.Services.AddRepositories();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(s =>
{
    s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = @"Jwt Authorization header using the Bearer scheme.
                      Enter 'Bearer' [space] and then your token in the text inpit below.
                      Example: 'Bearer secretKey'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = JwtBearerDefaults.AuthenticationScheme
    });

    s.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "OAuth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });

    #region Настройка документации Swagger

    var includeDocsTypesMarkers = new[]
    {
        typeof(UserInfoDto),
        typeof(UserShortInfoDto),
        typeof(UserCreateDto),
        typeof(UserEditDto),
        typeof(UserUpdatedDto),
        typeof(UserRoleDto),

        typeof(SubCategoryInfoDto),
        typeof(SubCategoryShortInfoDto),
        typeof(SubCategoryCreateDto),
        typeof(SubCategoryEditDto),

        typeof(CategoryInfoDto),
        typeof(CategoryShortInfoDto),
        typeof(CategoryCreateDto),
        typeof(CategoryEditDto),

        typeof(AttachmentInfoDto),
        typeof(AttachmentShortInfoDto),
        typeof(AttachmentUploadDto),
        typeof(AttachmentEditDto),

        typeof(AdvertisementInfoDto),
        typeof(AdvertisementShortInfoDto),
        typeof(AdvertisementCreateDto),
        typeof(AdvertisementEditDto),
        typeof(AdvertisementUpdatedDto),

        typeof(UserController),
        typeof(SubCategoryController),
        typeof(CategoryController),
        typeof(AttachmentController),
        typeof(AdvertisementController)
    };
    foreach (var marker in includeDocsTypesMarkers)
    {
        var xmlName = $"{marker.Assembly.GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlName);
        if (File.Exists(xmlPath))
            s.IncludeXmlComments(xmlPath);
    }

    #endregion
});

var app = builder.Build();

app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseMiddleware<BlockUserMiddleware>();

app.UseAuthorization();

app.MapControllers();

app.Run();