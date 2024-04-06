using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.Services.Files.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;

/// <inheritdoc />
public class AttachmentService : IAttachmentService
{
    private readonly IAttachmentRepository _attachmentRepository;
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="userService"></param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService, IMapper mapper,
        IUserService userService)
    {
        _attachmentRepository = attachmentRepository;
        _userService = userService;
        _fileService = fileService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attachmentEntity = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        var attachmentDto = _mapper.Map<AttachmentInfoDto>(attachmentEntity);
        return attachmentDto;
    }

    /// <inheritdoc />
    public async Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        var attachmentEntities =
            await _attachmentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        var attachmentDtos = _mapper.Map<List<AttachmentShortInfoDto>>(attachmentEntities);
        return attachmentDtos;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto> UploadByAdvertisementIdAsync(Guid advertisementId, Guid userId,
        AttachmentUploadDto uploadDto, CancellationToken cancellationToken)
    {
        var newAttachmentEntity = _mapper.Map<Attachment>(uploadDto);

        await _userService.CheckUserPermissionAsync(userId, newAttachmentEntity.Advertisement.UserId,
            cancellationToken);

        var fileUrl = await _fileService.UploadFileAsync(uploadDto.File, cancellationToken);
        newAttachmentEntity.Url = fileUrl;
        newAttachmentEntity.AdvertisementId = advertisementId;

        var uploadedAttachment = await _attachmentRepository.CreateAsync(newAttachmentEntity, cancellationToken);

        var dto = _mapper.Map<AttachmentShortInfoDto>(uploadedAttachment);
        return dto;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> UpdateByIdAsync(Guid id, Guid userId, AttachmentUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var attachmentEntity = await _attachmentRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        await _userService.CheckUserPermissionAsync(userId, attachmentEntity.Advertisement.UserId, cancellationToken);

        _mapper.Map(updateDto, attachmentEntity);

        var url = await _fileService.UploadFileAsync(updateDto.File, cancellationToken);
        attachmentEntity.Url = url;

        var updatedAttachment = await _attachmentRepository.UpdateAsync(attachmentEntity, cancellationToken);

        var updatedDto = _mapper.Map<AttachmentInfoDto>(updatedAttachment);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var attachmentEntity = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        await _userService.CheckUserPermissionAsync(userId, attachmentEntity.Advertisement.UserId, cancellationToken);

        await _attachmentRepository.DeleteByIdAsync(id, cancellationToken);
    }
}