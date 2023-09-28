using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    public AttachmentService(IAttachmentRepository attachmentRepository)
    {
        _attachmentRepository = attachmentRepository;
    }

    /// <inheritdoc />
    public Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _attachmentRepository.GetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AttachmentDto> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        return _attachmentRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public Task<AttachmentDto> CreateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        return _attachmentRepository.CreateAsync(dto, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AttachmentDto> UpdateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        return _attachmentRepository.UpdateAsync(dto, cancellationToken);
    }

    /// <inheritdoc />
    public Task<AttachmentDto> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return _attachmentRepository.DeleteByIdAsync(id, cancellationToken);
    }
}