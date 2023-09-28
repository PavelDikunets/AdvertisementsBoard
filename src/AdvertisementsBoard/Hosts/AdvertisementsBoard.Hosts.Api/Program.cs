using System.Text.Json;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Hosts.Api.Controllers;
using AdvertisementsBoard.Infrastructure.ComponentRegistrar;
using Microsoft.AspNetCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(s =>
{
    var includeDocsTypesMarkers = new[]
    {
        typeof(AttachmentInfoDto),
        typeof(AttachmentUploadDto),
        typeof(AdvertisementDto),
        typeof(AdvertisementInfoDto),
        typeof(AdvertisementCreateDto),
        typeof(AdvertisementUpdateDto),
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

            if (ex is InvalidOperationException)
            {
                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result).ConfigureAwait(false);
            }
            else if (ex is NotFoundException)
            {
                context.Response.StatusCode = 404;
                context.Response.ContentType = "application/json";

                var result = JsonSerializer.Serialize(new { error = ex.Message });
                await context.Response.WriteAsync(result).ConfigureAwait(false);
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

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();