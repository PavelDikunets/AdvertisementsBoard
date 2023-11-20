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
    /// <returns>Сущность объявления.</returns>
    Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Список объявлений с краткой информацией.</returns>
    Task<List<Advertisement>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize);

    /// <summary>
    ///     Получить все объявления пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список объявлений пользователя.</returns>
    Task<List<Advertisement>> GetAllByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="entity">Сущность объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления.</returns>
    Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить объявление.
    /// </summary>
    /// <param name="entity">Сущность объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность объявления.</returns>
    Task<Advertisement> UpdateAsync(Advertisement entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<Advertisement> FindByIdAsync(Guid id, CancellationToken cancellationToken);
}