using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Files.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    /// <param name="mapper">Маппер.</param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService,
        IAdvertisementService advertisementService, IMapper mapper)
    {
        _attachmentRepository = attachmentRepository;
        _fileService = fileService;
        _advertisementService = advertisementService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null) throw new NotFoundException($"Вложение с идентификатором {id} не найдено.");

        var infoDto = _mapper.Map<AttachmentInfoDto>(dto);
        return infoDto;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto[]> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        await _advertisementService.TryFindByIdAsync(advertisementId, cancellationToken);

        var dtos = await _attachmentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadByAdvertisementIdAsync(AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        await _advertisementService.TryFindByIdAsync(dto.AdvertisementId, cancellationToken);

        var fileUrl = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        var entity = _mapper.Map<Attachment>(dto);
        entity.Url = fileUrl;

        var id = await _attachmentRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto> UpdateByIdAsync(Guid id, AttachmentUpdateDto uploadDto,
        CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);

        var currentDto = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        var url = await _fileService.UploadFileAsync(uploadDto.File, cancellationToken);

        _mapper.Map(uploadDto, currentDto);
        currentDto.Url = url;

        var entity = _mapper.Map<Attachment>(currentDto);

        var dto = await _attachmentRepository.UpdateByIdAsync(entity, cancellationToken);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);
        await _attachmentRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private async Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _attachmentRepository.TryFindByIdAsync(id, cancellationToken);
        if (!exists) throw new NotFoundException($"Вложение с идентификатором {id} не найдено.");
    }
}