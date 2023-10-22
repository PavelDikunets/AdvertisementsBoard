using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Domain.Advertisements;
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
        _mapper = mapper;
        _userService = userService;
        _subCategoryService = subCategoryService;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null) throw new NotFoundException($"Объявление с идентификатором {id} не найдено.");

        var infoDto = _mapper.Map<AdvertisementInfoDto>(dto);
        return infoDto;
    }

    /// <inheritdoc />
    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        var dtos = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        await _userService.TryFindByIdAsync(dto.UserId, cancellationToken);
        await _subCategoryService.TryFindByIdAsync(dto.SubCategoryId, cancellationToken);

        var entity = _mapper.Map<Advertisement>(dto);

        var id = await _advertisementRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, AdvertisementUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);

        var currentDto = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        if (currentDto.User.Id != updateDto.UserId)
            throw new ForbiddenException("Нет прав на изменение этого объявления.");

        _mapper.Map(updateDto, currentDto);
        var entity = _mapper.Map<Advertisement>(currentDto);

        var updatedDto = await _advertisementRepository.UpdateAsync(entity, cancellationToken);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);
        await _advertisementRepository.DeleteByIdAsync(id, cancellationToken);
    }


    /// <inheritdoc />
    public async Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _advertisementRepository.TryFindByIdAsync(id, cancellationToken);
        if (!exists) throw new NotFoundException($"Объявление с идентификатором {id} не найдено.");
    }
}