using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Domain.Attachments;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AttachmentRepository : IAttachmentRepository
{
    private readonly IBaseDbRepository<Attachment> _repository;

    public AttachmentRepository(IBaseDbRepository<Attachment> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<Attachment[]> GetAllByIdAsync(Guid advertisementId, CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll().Where(e => e.AdvertisementId == advertisementId);

        var models = entities.Select(e => new Attachment
        {
            Url = e.Url
        });

        return await models.ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<Guid> UpdateByIdAsync(Attachment updatedEntity, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(updatedEntity.Id, cancellationToken);
        entity.Url = updatedEntity.Url;
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}