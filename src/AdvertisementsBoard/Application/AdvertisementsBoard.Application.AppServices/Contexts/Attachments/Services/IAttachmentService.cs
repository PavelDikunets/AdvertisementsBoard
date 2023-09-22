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
    /// <returns>Модель вложения <see cref="AttachmentDto" />.</returns>
    Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные вложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция вложений <see cref="AttachmentDto" />.</returns>
    public Task<AttachmentInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0);

    /// <summary>
    ///     Загрузить вложение.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор вложения <see cref="System.Guid" />.</returns>
    public Task<Guid> UploadByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> UpdateAsync(ExistingAttachmentUpdateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если удаление произошло успешно, false - в противном случае.</returns>
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}