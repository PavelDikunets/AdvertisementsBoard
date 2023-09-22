using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;

public interface IAttachmentRepository
{
    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" /></returns>
    Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить постраничные вложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция вложений <see cref="AdvertisementInfoDto" /></returns>
    public Task<AttachmentInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="entity">Сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="entity">Сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> UpdateByIdAsync();

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}