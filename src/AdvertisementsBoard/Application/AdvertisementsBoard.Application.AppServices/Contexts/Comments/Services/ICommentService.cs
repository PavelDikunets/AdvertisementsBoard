using AdvertisementsBoard.Contracts.Comments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;

/// <summary>
///     Сервис для работы с комментариями к объявлению.
/// </summary>
public interface ICommentService
{
    /// <summary>
    ///     Получить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией комментария.</returns>
    Task<CommentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все комментарии к объявлению.
    /// </summary>
    /// <param name="advertisementId">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список категорий с кратким описанием.</returns>
    Task<List<CommentInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать комментарий в объявлении.
    /// </summary>
    /// <param name="advertisementId">Идентификатор объявления.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="createDto">Модель создания комментари.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного комментария.</returns>
    Task<Guid> CreateAsync(Guid advertisementId, Guid userId, CommentCreateDto createDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить комментарий.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="updateDto">Модель редактирования комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией комментария.</returns>
    Task<CommentUpdatedDto> UpdateByIdAsync(Guid id, Guid userId, CommentUpdateDto updateDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="userId"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns></returns>
    Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}