using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Contracts.Attachments;
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
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        var model = new AttachmentInfoDto
        {
            FileName = entity.FileName
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        var entities = _repository.GetAll();

        var models = entities.Select(e => new AttachmentInfoDto
        {
            FileName = e.FileName
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
    public async Task<Guid> UpdateByIdAsync()
    {
        return Guid.NewGuid();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}