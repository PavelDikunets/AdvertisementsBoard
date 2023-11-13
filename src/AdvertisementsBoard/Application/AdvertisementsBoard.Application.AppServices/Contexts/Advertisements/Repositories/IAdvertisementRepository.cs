using System.Linq.Expressions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий для работы с объявлениями.
/// </summary>
public interface IAdvertisementRepository
{
    /// <summary>
    ///     Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления.</returns>
    Task<AdvertisementDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Список объявлений с краткой информацией.</returns>
    Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize);

    /// <summary>
    ///     Получить все объявления пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объявлений пользователя.</returns>
    Task<List<AdvertisementShortInfoDto>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель создания объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления.</returns>
    Task<Guid> CreateAsync(AdvertisementDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить объявление.
    /// </summary>
    /// <param name="dto">Модель объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным объявлением.</returns>
    Task<AdvertisementUpdatedDto> UpdateAsync(AdvertisementDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить объявление по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления.</returns>
    Task<AdvertisementDto> FindWhereAsync(Expression<Func<Advertisement, bool>> filter,
        CancellationToken cancellationToken);
}