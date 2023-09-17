using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Hosts.Api.Controllers;
using AdvertisementsBoard.Infrastructure.ComponentRegistrar;
using AdvertisementsBoard.Infrastructure.DataAccess;
using AdvertisementsBoard.Infrastructure.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

#region

// Регистрация конфигурации контекста базы данных.
builder.Services.AddSingleton<IDbContextOptionsConfigurator<BaseDbContext>, BaseDbContextConfiguration>();

builder.Services.AddDbContext<BaseDbContext>(
    (sp, dbOptions) => sp.GetRequiredService<IDbContextOptionsConfigurator<BaseDbContext>>()
        .Configure((DbContextOptionsBuilder<BaseDbContext>)dbOptions));

builder.Services.AddScoped((Func<IServiceProvider, DbContext>)(sp => sp.GetRequiredService<BaseDbContext>()));

#endregion

builder.Services.AddServices();
builder.Services.AddRepositories();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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