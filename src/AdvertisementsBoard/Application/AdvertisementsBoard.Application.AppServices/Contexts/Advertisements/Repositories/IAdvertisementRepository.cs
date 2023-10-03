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
    /// <returns>Сущность объявления <see cref="Advertisement" />.</returns>
    Task<Advertisement> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <returns>Перечеслитель сущностей объявлений <see cref="Advertisement" />.</returns>
    Task<IEnumerable<Advertisement>> GetAllAsync(CancellationToken cancellationToken, int pageNumber,
        int pageSize);

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="entity">Сущность объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного объявления.</returns>
    Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateAsync(Advertisement updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}