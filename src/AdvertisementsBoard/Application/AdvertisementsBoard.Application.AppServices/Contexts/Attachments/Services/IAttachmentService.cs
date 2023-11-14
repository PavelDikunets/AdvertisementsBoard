using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <summary>
///     Сервис для работы с вложениями.
/// </summary>
public interface IAttachmentService
{
    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией о вложении.</returns>
    Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список вложений с краткой информацией.</returns>
    Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Загрузить вложение по идентификатору объявления.
    /// </summary>
    /// <param name="advertisementId"></param>
    /// <param name="userId"></param>
    /// <param name="dto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор загруженного вложения.</returns>
    Task<AttachmentShortInfoDto> UploadByAdvertisementIdAsync(Guid advertisementId, Guid userId,
        AttachmentUploadDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="editDto">Модель обновления вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным вложением.</returns>
    Task<AttachmentInfoDto> UpdateByIdAsync(Guid id, Guid userId, AttachmentEditDto editDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken);
}