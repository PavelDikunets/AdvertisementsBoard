using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;
using AdvertisementsBoard.Domain.Attachments;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;

/// <summary>
///     Репозиторий вложений.
/// </summary>
public class AttachmentRepository : IAttachmentRepository
{
    private readonly IBaseDbRepository<Attachment> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий вложений.</param>
    public AttachmentRepository(IBaseDbRepository<Attachment> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Attachment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attachment = await TryGetByIdAsync(id, cancellationToken);

        return attachment;
    }

    /// <inheritdoc />
    public async Task<List<Attachment>> GetAllByAdvertisementIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var listAttachments = await _repository.GetAll()
            .Where(a => a.AdvertisementId == id)
            .ToListAsync(cancellationToken);

        return listAttachments;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<Attachment> UpdateAsync(Attachment entity,
        CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attachment = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(attachment, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Attachment> FindWhereAsync(Expression<Func<Attachment, bool>> filter,
        CancellationToken cancellationToken)
    {
        var attachment = await _repository.FindWhereAsync(filter)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (attachment == null) throw new AttachmentNotFoundException();
        return attachment;
    }

    private async Task<Attachment> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);

        if (account == null) throw new AttachmentNotFoundException(id);
        return account;
    }
}