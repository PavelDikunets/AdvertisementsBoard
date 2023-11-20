using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;
using AutoMapper;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly ILogger<AdvertisementService> _logger;
    private readonly IMapper _mapper;
    private readonly ISubCategoryService _subCategoryService;
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementService" />.
    /// </summary>
    /// <param name="advertisementRepository">Репозиторий для работы с объявлениями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    /// <param name="subCategoryService">Сервис для работы с подкатегориями.</param>
    /// <param name="logger">Логгер.</param>
    public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper,
        IUserService userService,
        ISubCategoryService subCategoryService, ILogger<AdvertisementService> logger)
    {
        _advertisementRepository = advertisementRepository;
        _subCategoryService = subCategoryService;
        _logger = logger;
        _userService = userService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisementEntity = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        var advertisementDto = _mapper.Map<AdvertisementInfoDto>(advertisementEntity);
        return advertisementDto;
    }

    public async Task<List<AdvertisementShortInfoDto>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var advertisementEntities = await _advertisementRepository.GetAllByUserIdAsync(userId, cancellationToken);

        var avertisementDtos = _mapper.Map<List<AdvertisementShortInfoDto>>(advertisementEntities);
        return avertisementDtos;
    }

    /// <inheritdoc />
    public async Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        var advertisementEntities = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);

        var avertisementDtos = _mapper.Map<List<AdvertisementShortInfoDto>>(advertisementEntities);
        return avertisementDtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, Guid userId,
        CancellationToken cancellationToken)
    {
        await _userService.DoesUserExistByIdAsync(userId, cancellationToken);

        await _subCategoryService.DoesSubCategoryExistByIdAsync(dto.SubCategoryId, cancellationToken);

        var newAdvertisementEntity = _mapper.Map<Advertisement>(dto);
        newAdvertisementEntity.UserId = userId;
        newAdvertisementEntity.IsActive = true;

        var createdAdvertisementId =
            await _advertisementRepository.CreateAsync(newAdvertisementEntity, cancellationToken);

        return createdAdvertisementId;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, Guid userId,
        AdvertisementUpdateDto advertisementUpdateDto,
        CancellationToken cancellationToken)
    {
        var advertisementEntity = await _advertisementRepository.FindByIdAsync(id, cancellationToken);

        _logger.LogInformation("Запрос обновления объявления: '{Advertisement}.",
            JsonConvert.SerializeObject(advertisementEntity));

        await _userService.CheckUserPermissionAsync(userId, advertisementEntity.UserId, cancellationToken);

        _mapper.Map(advertisementUpdateDto, advertisementEntity);
        advertisementEntity.Id = id;
        advertisementEntity.UserId = userId;

        var updatedAdvertisementEntity =
            await _advertisementRepository.UpdateAsync(advertisementEntity, cancellationToken);

        var updatedAdvertisementDto = _mapper.Map<AdvertisementUpdatedDto>(updatedAdvertisementEntity);
        return updatedAdvertisementDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var advertisementEntity = await _advertisementRepository.FindByIdAsync(id, cancellationToken);

        await _userService.CheckUserPermissionAsync(userId, advertisementEntity.UserId, cancellationToken);

        await _advertisementRepository.DeleteByIdAsync(id, cancellationToken);
    }
}