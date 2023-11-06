using System.Text.Json;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Passwords.ErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Hosts.Api.Controllers;
using AdvertisementsBoard.Infrastructure.ComponentRegistrar;
using Microsoft.AspNetCore.Diagnostics;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Elasticsearch()
    .CreateLogger();

builder.Host.UseSerilog();

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(s =>
{
    var includeDocsTypesMarkers = new[]
    {
        typeof(UserInfoDto),
        typeof(UserShortInfoDto),
        typeof(UserCreateDto),
        typeof(UserUpdateDto),
        typeof(UserUpdatedDto),

        typeof(SubCategoryInfoDto),
        typeof(SubCategoryShortInfoDto),
        typeof(SubCategoryCreateDto),
        typeof(SubCategoryUpdateDto),
        typeof(SubCategoryUpdatedDto),

        typeof(CategoryInfoDto),
        typeof(CategoryShortInfoDto),
        typeof(CategoryCreateDto),
        typeof(CategoryUpdateDto),
        typeof(CategoryUpdatedDto),

        typeof(AttachmentInfoDto),
        typeof(AttachmentShortInfoDto),
        typeof(AttachmentUploadDto),
        typeof(AttachmentUpdateDto),
        typeof(AttachmentUpdatedDto),

        typeof(AdvertisementInfoDto),
        typeof(AdvertisementShortInfoDto),
        typeof(AdvertisementCreateDto),
        typeof(AdvertisementUpdateDto),
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
});

var app = builder.Build();

app.UseExceptionHandler(errorApp =>
{
    errorApp.Run(async context =>
    {
        context.Response.StatusCode = 500;
        context.Response.ContentType = "application/json";

        var error = context.Features.Get<IExceptionHandlerFeature>();
        if (error != null)
        {
            var ex = error.Error;

            switch (ex)
            {
                case InvalidOperationException:
                {
                    var result = JsonSerializer.Serialize(new { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
                case AdvertisementNotFoundByIdException or AttachmentNotFoundByIdException
                    or CategoryNotFoundByIdException
                    or SubCategoryNotFoundByIdException or UserNotFoundByIdException:
                {
                    context.Response.StatusCode = 404;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
                case CategoryAlreadyExistsException or SubCategoryAlreadyExistsException
                    or UserAlreadyExistsByEmailException:
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
                case PasswordMismatchException:
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
                case AdvertisementForbiddenException:
                {
                    context.Response.StatusCode = 403;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
                default:
                {
                    context.Response.StatusCode = 500;
                    context.Response.ContentType = "application/json";

                    var result = JsonSerializer.Serialize(new
                        { message = ex.Message });
                    await context.Response.WriteAsync(result).ConfigureAwait(false);
                    break;
                }
            }
        }
    });
});

app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();