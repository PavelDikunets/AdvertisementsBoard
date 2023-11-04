using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Files.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    private readonly ILogger<AttachmentService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="logger">Логирование сервиса <see cref="AttachmentService" />.</param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService,
        IAdvertisementService advertisementService, IMapper mapper, ILogger<AttachmentService> logger)
    {
        _attachmentRepository = attachmentRepository;
        _advertisementService = advertisementService;
        _fileService = fileService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение вложения по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        var infoDto = _mapper.Map<AttachmentInfoDto>(dto);

        _logger.LogInformation("Вложение успешно получено по Id: '{Id}'.", id);

        return infoDto;
    }

    /// <inheritdoc />
    public async Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение коллекции вложений в объявлении с Id: '{AdvertisementId}'.", advertisementId);

        await _advertisementService.EnsureAdvertisementExistsByIdAsync(advertisementId, cancellationToken);

        var dtos = await _attachmentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        _logger.LogInformation("Коллекция вложений успешно получена.");

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> UploadByAdvertisementIdAsync(AttachmentUploadDto uploadDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание вложения в объявлении с Id: '{AdvertisementId};.", uploadDto.AdvertisementId);

        await _advertisementService.EnsureAdvertisementExistsByIdAsync(uploadDto.AdvertisementId, cancellationToken);

        var dto = _mapper.Map<AttachmentDto>(uploadDto);

        var fileUrl = await _fileService.UploadFileAsync(uploadDto.File, cancellationToken);

        dto.Url = fileUrl;

        var id = await _attachmentRepository.CreateAsync(dto, cancellationToken);

        _logger.LogInformation("Вложение Url: '{Url}' Id: '{Id}' успешно создано.", dto.Url, id);

        return id;
    }

    /// <inheritdoc />
    public async Task<AttachmentUpdatedDto> UpdateByIdAsync(Guid id, AttachmentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление вложения по Id: '{Id}'.", id);

        var currentDto = await _attachmentRepository.GetWhereAsync(a => a.Id == id, cancellationToken);

        if (currentDto == null)
        {
            _logger.LogInformation("Вложение не найдено по Id: '{Id}'.", id);
            throw new AttachmentNotFoundByIdException(id);
        }

        _mapper.Map(dto, currentDto);

        var url = await _fileService.UploadFileAsync(dto.File, cancellationToken);

        currentDto.Url = url;

        var updatedDto = await _attachmentRepository.UpdateAsync(currentDto, cancellationToken);

        _logger.LogInformation("Вложение успешно обновлено Url: '{updatedUrl}' Id: '{Id}'.",
            updatedDto.Url, id);

        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление вложения по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        await _attachmentRepository.DeleteByIdAsync(dto.Id, cancellationToken);

        _logger.LogWarning("Вложение удалено по Id: '{id}'.", id);
    }


    private async Task<AttachmentDto> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        if (dto != null) return dto;

        _logger.LogInformation("Вложение не найдено по Id: '{Id}'", id);
        throw new AttachmentNotFoundByIdException(id);
    }
}