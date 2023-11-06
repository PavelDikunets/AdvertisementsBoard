using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using AutoMapper;
using Microsoft.Extensions.Logging;

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
    /// <param name="logger">Логирование сервиса <see cref="AdvertisementService" />.</param>
    public AdvertisementService(IAdvertisementRepository advertisementRepository, IMapper mapper,
        IUserService userService,
        ISubCategoryService subCategoryService, ILogger<AdvertisementService> logger)
    {
        _advertisementRepository = advertisementRepository;
        _mapper = mapper;
        _userService = userService;
        _subCategoryService = subCategoryService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение объявления по Id: '{Id}'.", id);

        var dto = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null)
        {
            _logger.LogInformation("Объявление не найдено по Id: '{Id}'", id);
            throw new AdvertisementNotFoundByIdException(id);
        }

        var infoDto = _mapper.Map<AdvertisementInfoDto>(dto);

        _logger.LogInformation("Объявление успешно получено по Id: '{Id}'.", id);

        return infoDto;
    }

    /// <inheritdoc />
    public async Task<List<AdvertisementShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        _logger.LogInformation("Получение коллекции объявлений.");

        var dtos = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);

        _logger.LogInformation("Коллекция объявлений успешно получена.");

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание объявления '{Title}'.", dto.Title);

        await _userService.EnsureUserExistsByIdAsync(dto.UserId, cancellationToken);

        await _subCategoryService.EnsureSubCategoryExistsByIdAsync(dto.SubCategoryId, cancellationToken);

        var id = await _advertisementRepository.CreateAsync(dto, cancellationToken);

        _logger.LogInformation("Объявление '{Title}' Id: '{Id}' успешно создано.", dto.Title, id);

        return id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdatedDto> UpdateByIdAsync(Guid id, AdvertisementUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление объявления по Id: '{Id}'.", id);

        await _userService.EnsureUserExistsByIdAsync(updateDto.UserId, cancellationToken);

        var currentDto = await _advertisementRepository.GetWhereAsync(a => a.Id == id, cancellationToken);

        if (currentDto == null)
        {
            _logger.LogInformation("Объявление не найдено по Id: '{Id}'", id);
            throw new AdvertisementNotFoundByIdException(id);
        }

        if (currentDto.User.Id != updateDto.UserId)
        {
            _logger.LogInformation("Нет прав на изменение этого объявления.");
            throw new AdvertisementForbiddenException();
        }

        var currentTitle = currentDto.Title;

        _mapper.Map(updateDto, currentDto);

        var updatedDto = await _advertisementRepository.UpdateAsync(currentDto, cancellationToken);

        _logger.LogInformation("Объявление '{Title}' успешно обновлено на '{updatedTitle}' Id: '{Id}'.",
            currentTitle, updatedDto.Title, id);

        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление объявления по Id: '{Id}'.", id);

        await EnsureAdvertisementExistsByIdAsync(id, cancellationToken);

        await _advertisementRepository.DeleteByIdAsync(id, cancellationToken);

        _logger.LogWarning("Объявление удалено по Id: '{Id}'.", id);
    }

    /// <inheritdoc />
    public async Task EnsureAdvertisementExistsByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос существования объявления по Id: '{Id}'.", id);

        var exist =
            await _advertisementRepository.DoesAdvertisementExistWhereAsync(a => a.Id == id, cancellationToken);
        if (!exist)
        {
            _logger.LogInformation("Объявление не найдено по Id: '{Id}'", id);
            throw new AdvertisementNotFoundByIdException(id);
        }
    }
}