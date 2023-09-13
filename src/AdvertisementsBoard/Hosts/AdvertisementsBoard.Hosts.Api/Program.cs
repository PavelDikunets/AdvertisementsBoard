using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Hosts.Api.Controllers;
using AdvertisementsBoard.Infrastructure.DataAccess;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(s =>
{
    var includeDocsTypesMarkers = new[]
    {
        typeof(AdvertisementDto),
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

#region База данных: конфигурация и регистрация контекста

// Добавляем DbContext
builder.Services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextConfiguration>();

builder.Services.AddDbContext<BaseDbContext>((Action<IServiceProvider, DbContextOptionsBuilder>)
    ((sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>()
        .Configure((DbContextOptionsBuilder<BaseDbContext>)dbOptions)));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BaseDbContext>()));

#endregion

// Регистрация open generic репозитория
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


// Сервис и репозиторий для работы с объявлениями.
builder.Services.AddTransient<IAdvertisementService, AdvertisementService>();
builder.Services.AddTransient<IAdvertisementRepository, AdvertisementRepository>();

// Сервис и репозиторий для работы с вложениями.
builder.Services.AddTransient<IAttachmentService, AttachmentService>();
builder.Services.AddTransient<IAttachmentRepository, AttachmentRepository>();

var app = builder.Build();

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