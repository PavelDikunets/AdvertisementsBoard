using AdvertisementsBoard.Domain.Comments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Comments.Repositories;

/// <summary>
///     Репозиторий для работы с комментариями.
/// </summary>
public interface ICommentRepository
{
    /// <summary>
    ///     Получить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель комментария.</returns>
    Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все комментарии к объявлению.
    /// </summary>
    /// <param name="id">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список комментариев с краткой информацией.</returns>
    Task<List<Comment>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать комментарий.
    /// </summary>
    /// <param name="comment"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного комментария.</returns>
    Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить комментарий.
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным комментарием.</returns>
    Task<Comment> UpdateAsync(Comment entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}