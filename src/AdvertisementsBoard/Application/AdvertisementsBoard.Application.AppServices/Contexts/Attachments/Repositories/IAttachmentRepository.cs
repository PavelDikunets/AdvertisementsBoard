using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;

public interface IAttachmentRepository
{
    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementDto" /></returns>
    Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все вложения у объявления.
    /// </summary>
    /// <param name="advertisementId">Идентификтор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив вложений <see cref="Attachment" />.</returns>
    public Task<Attachment[]> GetAllByIdAsync(Guid advertisementId, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать вложение.
    /// </summary>
    /// <param name="updatedEntity">Сущность вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<Guid> CreateAsync(Attachment updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="updatedEntity">Сущность вложения.</param>
    public Task<Guid> UpdateByIdAsync(Attachment updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    public Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}