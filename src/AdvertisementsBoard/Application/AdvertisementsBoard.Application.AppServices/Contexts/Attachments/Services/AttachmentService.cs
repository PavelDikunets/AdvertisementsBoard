using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Files.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    /// <param name="advertisementRepository"></param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService,
        IAdvertisementRepository advertisementRepository)
    {
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
        _advertisementRepository = advertisementRepository;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentRepository.GetByIdAsync(id, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        return await _attachmentRepository.GetAllAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> UploadByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var advertDto = await _advertisementRepository.GetByIdAsync(id, cancellationToken);
        if (advertDto == null) throw new Exception("Объявление не найдено");

        var fileName = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        var entity = new Attachment
        {
            FileName = fileName,
            AdvertisementId = id
        };

        return await _attachmentRepository.CreateAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> UpdateAsync(ExistingAttachmentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = new Attachment
        {
            Id = dto.Id,
            FileName = dto.FileName
        };
        return await _attachmentRepository.UpdateByIdAsync();
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentRepository.DeleteByIdAsync(id, cancellationToken);
        return result;
    }
}