using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contracts.Advertisements.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AdvertisementRepository : IAdvertisementRepository
{
    public Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.Run(() => new AdvertisementDto
        {
            Id = Guid.NewGuid(),
            Title = "Тестовый заголовок.",
            Description = "Тестовое описание.",
            Price = 101010101010.101M,
            TagNames = new[] { "ТестТег1", "ТестТег2" },
            CategoryName = "Тестовая категория."
        }, cancellationToken);
    }
}