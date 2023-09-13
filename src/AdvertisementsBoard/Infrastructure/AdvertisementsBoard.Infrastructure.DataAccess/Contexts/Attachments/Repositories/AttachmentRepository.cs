using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AttachmentRepository : IAttachmentRepository
{
    /// <inheritdoc />
    public Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.Run(() => new AttachmentDto
        {
            Id = Guid.NewGuid(),
            FilePath = "/example/filePath/"
        }, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AttachmentDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10, int pageIndex = 0)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<AttachmentDto> CreateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<AttachmentDto> UpdateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        return null;
    }

    /// <inheritdoc />
    public Task<AttachmentDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return null;
    }
}