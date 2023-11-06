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
    /// <returns>Модель с информацией о вложении <see cref="AttachmentInfoDto" />.</returns>
    Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список вложений с краткой информацией <see cref="AttachmentShortInfoDto" />.</returns>
    Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Загрузить вложение по идентификатору объявления.
    /// </summary>
    /// <param name="dto">Модель загрузки вложения <see cref="AttachmentUploadDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор загруженного вложения.</returns>
    Task<Guid> UploadByAdvertisementIdAsync(AttachmentUploadDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="dto">Модель обновления вложения <see cref="AttachmentUpdateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным вложением <see cref="AttachmentUpdatedDto" />.</returns>
    Task<AttachmentUpdatedDto> UpdateByIdAsync(Guid id, AttachmentUpdateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}