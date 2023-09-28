using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Contracts.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService)
    {
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetAttachmentFromRepositoryById(id, cancellationToken);

        var dto = new AttachmentInfoDto
        {
            Url = entity.Url
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto[]> GetAllByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entities = await _attachmentRepository.GetAllByIdAsync(id, cancellationToken);
        var dto = entities.Select(e => new AttachmentInfoDto
        {
            Url = e.Url
        }).ToArray();

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await GetAttachmentFromRepositoryById(id, cancellationToken);

        var fileName = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        entity.Url = fileName;
        entity.AdvertisementId = id;

        var result = await _attachmentRepository.CreateAsync(entity, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<Guid> UpdateByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await GetAttachmentFromRepositoryById(id, cancellationToken);

        var attachmentUrl = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        entity.Id = id;
        entity.Url = attachmentUrl;

        var result = await _attachmentRepository.UpdateByIdAsync(entity, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await GetAttachmentFromRepositoryById(id, cancellationToken);
        await _attachmentRepository.DeleteByIdAsync(entity.Id, cancellationToken);
        return true;
    }


    private async Task<Attachment> GetAttachmentFromRepositoryById(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Объявление с идентификатором {id} не найдено.");

        return entity;
    }
}