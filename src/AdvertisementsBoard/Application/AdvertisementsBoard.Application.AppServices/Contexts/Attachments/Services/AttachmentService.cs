using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.Services.Files.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;
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
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentService" />
    /// </summary>
    /// <param name="attachmentRepository">Репозиторий для работы с вложениями.</param>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="fileService">Сервис для работы с файлами.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="userService"></param>
    public AttachmentService(IAttachmentRepository attachmentRepository, IFileService fileService,
        IAdvertisementService advertisementService, IMapper mapper, IUserService userService)
    {
        _attachmentRepository = attachmentRepository;
        _advertisementService = advertisementService;
        _userService = userService;
        _fileService = fileService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AttachmentInfoDto>(attachment);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        var listAttachments =
            await _attachmentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        var attachmentDtos = _mapper.Map<List<AttachmentShortInfoDto>>(listAttachments);
        return attachmentDtos;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto> UploadByAdvertisementIdAsync(Guid advertisementId, Guid userId,
        AttachmentUploadDto uploadDto, CancellationToken cancellationToken)
    {
        await ValidateUserAsync(advertisementId, userId, cancellationToken);

        var attachment = _mapper.Map<Attachment>(uploadDto);

        var fileUrl = await _fileService.UploadFileAsync(uploadDto.File, cancellationToken);
        attachment.Url = fileUrl;
        attachment.AdvertisementId = advertisementId;

        var uploadedAttachment = await _attachmentRepository.CreateAsync(attachment, cancellationToken);

        var dto = _mapper.Map<AttachmentShortInfoDto>(uploadedAttachment);
        return dto;
    }

    /// <inheritdoc />
    public async Task<AttachmentInfoDto> UpdateByIdAsync(Guid id, Guid userId, AttachmentEditDto editDto,
        CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        await ValidateUserAsync(attachment.AdvertisementId, userId, cancellationToken);

        _mapper.Map(editDto, attachment);

        var url = await _fileService.UploadFileAsync(editDto.File, cancellationToken);
        attachment.Url = url;

        var updatedAttachment = await _attachmentRepository.UpdateAsync(attachment, cancellationToken);

        var updatedDto = _mapper.Map<AttachmentInfoDto>(updatedAttachment);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var attachment = await _attachmentRepository.GetByIdAsync(id, cancellationToken);

        await ValidateUserAsync(attachment.AdvertisementId, userId, cancellationToken);

        await _attachmentRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private async Task ValidateUserAsync(Guid advertisementId, Guid userId, CancellationToken cancellationToken)
    {
        var userIdFromAdvertisement = await _advertisementService.GetUserIdAsync(advertisementId, cancellationToken);

        var isValid = await _userService.ValidateUserAsync(userId, userIdFromAdvertisement, cancellationToken);

        if (!isValid) throw new AttachmentForbiddenException();
    }
}