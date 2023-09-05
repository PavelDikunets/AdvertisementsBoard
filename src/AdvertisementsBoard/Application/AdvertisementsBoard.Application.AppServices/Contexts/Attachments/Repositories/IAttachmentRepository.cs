using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;

public interface IAttachmentRepository
{
    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementDto" /></returns>
    Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные вложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция вложений <see cref="AdvertisementDto" /></returns>
    public Task<AttachmentDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="dto">Модель объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AttachmentDto> CreateAsync(AttachmentDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AttachmentDto> UpdateAsync(AttachmentDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<AttachmentDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}