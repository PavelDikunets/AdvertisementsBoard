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
    /// <returns>Модель вложения <see cref="AttachmentInfoDto" />.</returns>
    Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения у объявления.
    /// </summary>
    /// <param name="advertisementId">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив вложений <see cref="AttachmentInfoDto" />.</returns>
    public Task<AttachmentInfoDto[]> GetAllByIdAsync(Guid advertisementId, CancellationToken cancellationToken);

    /// <summary>
    ///     Загрузить вложение.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор вложения.</returns>
    public Task<Guid> UploadByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="dto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор вложения.</returns>
    public Task<Guid> UpdateByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если удаление произошло успешно, false - в противном случае.</returns>
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}