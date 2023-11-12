using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;
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
    public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper,
        IUserService userService,
        ISubCategoryService subCategoryService)
    {
        _advertisementRepository = advertisementRepository;
        _subCategoryService = subCategoryService;
        _userService = userService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AdvertisementInfoDto>(advertisement);
        return dto;
    }

    public async Task<List<AdvertisementShortInfoDto>> GetAllByUserIdAsync(Guid userId,
        CancellationToken cancellationToken)
    {
        var listAdvertisements = await _advertisementRepository.GetAllByUserIdAsync(userId, cancellationToken);
        return listAdvertisements;
    }

    /// <inheritdoc />
    public async Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        var listAdvertisements = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);
        return listAdvertisements;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, Guid userId, CancellationToken cancellationToken)
    {
        await _userService.DoesUserExistByIdAsync(userId, cancellationToken);

        await _subCategoryService.DoesSubCategoryExistByIdAsync(dto.SubCategoryId, cancellationToken);

        var advertDto = _mapper.Map<AdvertisementDto>(dto);
        advertDto.UserId = userId;
        advertDto.IsActive = true;

        var id = await _advertisementRepository.CreateAsync(advertDto, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, Guid userId, AdvertisementEditDto editDto,
        CancellationToken cancellationToken)
    {
        var currentAdvertisement = await _advertisementRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        await ValidateUserAsync(id, userId, cancellationToken);

        _mapper.Map(editDto, currentAdvertisement);

        var updatedDto = await _advertisementRepository.UpdateAsync(currentAdvertisement, cancellationToken);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var currentAdvertisement = await _advertisementRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        await ValidateUserAsync(userId, currentAdvertisement.UserId, cancellationToken);

        await _advertisementRepository.DeleteByIdAsync(currentAdvertisement.Id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> GetUserIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var advertisement = await _advertisementRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        return advertisement.UserId;
    }


    private async Task ValidateUserAsync(Guid userId, Guid userIdFromAdvertisement, CancellationToken cancellationToken)
    {
        var isValid = await _userService.ValidateUserAsync(userId, userIdFromAdvertisement, cancellationToken);

        if (!isValid) throw new AdvertisementForbiddenException();
    }
}