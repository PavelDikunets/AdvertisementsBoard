using System.Linq.Expressions;
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
    /// <returns>Сущность вложения.</returns>
    Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список вложений.</returns>
    Task<List<Attachment>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="entity">Сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного вложения.</returns>
    Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение.
    /// </summary>
    /// <param name="entity">Сущность вложения..</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность вложения.</returns>
    Task<Attachment> UpdateAsync(Attachment entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить вложение по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Сущность вложения.</returns>
    Task<Attachment> FindWhereAsync(Expression<Func<Attachment, bool>> filter, CancellationToken cancellationToken);
}