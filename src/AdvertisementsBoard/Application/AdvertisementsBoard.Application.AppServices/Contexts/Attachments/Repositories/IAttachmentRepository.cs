using AdvertisementsBoard.Domain.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;

/// <summary>
///     Репозиторий для работы с вложениями.
/// </summary>
public interface IAttachmentRepository
{
    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность вложения <see cref="Attachment" />.</returns>
    Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив вложений <see cref="Attachment" />.</returns>
    Task<IEnumerable<Attachment>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной сущности вложения.</returns>
    Task<Guid> CreateAsync(Attachment updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task UpdateByIdAsync(Attachment updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}