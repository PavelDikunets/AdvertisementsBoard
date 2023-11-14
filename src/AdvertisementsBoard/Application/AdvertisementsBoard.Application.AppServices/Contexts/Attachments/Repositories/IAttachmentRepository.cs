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
    /// <returns>Модель вложения.</returns>
    Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список вложений с краткой информацией.</returns>
    Task<List<Attachment>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <returns>Идентификатор созданного вложения.</returns>
    Task<Attachment> CreateAsync(Attachment entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение.
    /// </summary>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным вложением.</returns>
    Task<Attachment> UpdateAsync(Attachment dto, CancellationToken cancellationToken);

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
    /// <returns>Модель вложения.</returns>
    Task<Attachment> FindWhereAsync(Expression<Func<Attachment, bool>> filter, CancellationToken cancellationToken);
}