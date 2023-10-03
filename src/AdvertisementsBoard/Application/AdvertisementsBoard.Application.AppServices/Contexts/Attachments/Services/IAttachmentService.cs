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
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив вложений <see cref="AttachmentInfoDto" />.</returns>
    Task<AttachmentInfoDto[]> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Загрузить вложение по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор вложения <see cref="Guid" />.</returns>
    Task<Guid> UploadByAdvertisementIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="uploadDto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель вложений <see cref="AttachmentInfoDto" />.</returns>
    Task<AttachmentInfoDto> UpdateByIdAsync(Guid id, AttachmentUploadDto uploadDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}