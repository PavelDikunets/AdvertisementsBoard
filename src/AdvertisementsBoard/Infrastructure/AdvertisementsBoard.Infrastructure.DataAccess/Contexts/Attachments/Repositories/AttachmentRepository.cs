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
    public async Task<IEnumerable<Attachment>> GetAllByAdvertisementIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().Where(a => a.AdvertisementId == id)
            .ToArrayAsync(cancellationToken);

        return entities;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateByIdAsync(Attachment updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }
}