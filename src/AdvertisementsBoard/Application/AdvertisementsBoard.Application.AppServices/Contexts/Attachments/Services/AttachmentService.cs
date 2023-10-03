using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Files.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService,
        IAdvertisementService advertisementService)
    {
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
        _advertisementService = advertisementService;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        var dto = new AttachmentInfoDto
        {
            Url = entity.Url
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto[]> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        await _advertisementService.GetByIdAsync(advertisementId, cancellationToken);

        var entities =
            await _attachmentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        var dtos = entities.Select(s => new AttachmentInfoDto
        {
            Url = s.Url
        }).ToArray();

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadByAdvertisementIdAsync(Guid advertisementId, AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        await _advertisementService.GetByIdAsync(advertisementId, cancellationToken);

        var fileUrl = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        var entity = new Attachment
        {
            Url = fileUrl,
            AdvertisementId = advertisementId
        };

        var id = await _attachmentRepository.CreateAsync(entity, cancellationToken);
        return id;
    }


    /// <inheritdoc />
    public async Task<AttachmentInfoDto> UpdateByIdAsync(Guid id, AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        var fileUrl = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        entity.Id = id;
        entity.Url = fileUrl;

        var uploadDto = new AttachmentInfoDto
        {
            Url = entity.Url
        };

        await _attachmentRepository.UpdateByIdAsync(entity, cancellationToken);
        return uploadDto;
    }


    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        await _attachmentRepository.DeleteByIdAsync(entity.Id, cancellationToken);
    }


    private async Task<Attachment> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Вложение с идентификатором {id} не найдено.");
        return entity;
    }
}