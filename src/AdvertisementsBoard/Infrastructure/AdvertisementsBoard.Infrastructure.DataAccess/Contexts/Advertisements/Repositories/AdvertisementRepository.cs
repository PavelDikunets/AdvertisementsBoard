using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly List<Advertisement> _advertisements = new();

    /// <inheritdoc />
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

    /// <inheritdoc />
    public Task<AdvertisementDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<Guid> CreateAsync(Advertisement model, CancellationToken cancellationToken)
    {
        model.Id = Guid.NewGuid();
        _advertisements.Add(model);
        return Task.Run(() => model.Id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AdvertisementDto> UpdateAsync(Advertisement model, CancellationToken cancellationToken)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<AdvertisementDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }
}