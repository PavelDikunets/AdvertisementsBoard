using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Contracts.SubCategories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;

/// <inheritdoc />
public class SubCategoryService : ISubCategoryService
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<SubCategoryService> _logger;
    private readonly IMapper _mapper;
    private readonly ISubCategoryRepository _subCategoryRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryService" />
    /// </summary>
    /// <param name="subCategoryRepository">Репозиторий для работы с подкатегориями.</param>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="logger">Логирование сервиса <see cref="SubCategoryService" />.</param>
    public SubCategoryService(ISubCategoryRepository subCategoryRepository, ICategoryService categoryService,
        IMapper mapper, ILogger<SubCategoryService> logger)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryService = categoryService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение подкатегории по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        var infoDto = _mapper.Map<SubCategoryInfoDto>(dto);

        _logger.LogInformation("Подкатегория успешно получена по Id: '{Id}'.", id);

        return infoDto;
    }

    /// <inheritdoc />
    public async Task<List<SubCategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение коллекции подкатегорий.");

        var dtos = await _subCategoryRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("Коллекция подкатегорий успешно получена.");

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание подкатегории '{Name}' в категории с Id: '{CategoryId}'.",
            dto.Name, dto.CategoryId);

        await _categoryService.EnsureCategoryExistsByIdAsync(dto.CategoryId, cancellationToken);

        await CheckSubCategoryNameExistence(dto.CategoryId, dto.Name, cancellationToken);

        var id = await _subCategoryRepository.CreateAsync(dto, cancellationToken);

        _logger.LogInformation("Подкатегория '{Name}' Id: '{Id}' успешно создана в категории с Id: '{CategoryId}'.",
            dto.Name, id, dto.CategoryId);

        return id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryUpdatedDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление подкатегории по Id: '{Id}'.", id);

        var currentDto = await _subCategoryRepository.GetWhereAsync(s => s.Id == id, cancellationToken);

        if (currentDto == null)
        {
            _logger.LogInformation("Подкатегория не найдена по Id: '{Id}'.", id);
            throw new SubCategoryNotFoundByIdException(id);
        }

        var currentSubCategoryName = currentDto.Name;

        await CheckSubCategoryNameExistence(currentDto.CategoryId, dto.Name, cancellationToken);

        _mapper.Map(dto, currentDto);

        var updatedDto = await _subCategoryRepository.UpdateAsync(currentDto, cancellationToken);

        _logger.LogInformation("Подкатегория '{Name}' успешно обновлена на '{updatedName}' Id: '{Id}'.",
            currentSubCategoryName, updatedDto.Name, id);

        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление подкатегории по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        await _subCategoryRepository.DeleteByIdAsync(dto.Id, cancellationToken);

        _logger.LogWarning("Подкатегория удалена по Id: '{id}'.", id);
    }

    /// <inheritdoc />
    public async Task EnsureSubCategoryExistsByIdAsync(Guid subCategoryId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос существования подкатегории по Id: '{Id}'.", subCategoryId);

        var exist = await _subCategoryRepository.DoesCategoryExistWhereAsync(u => u.Id == subCategoryId,
            cancellationToken);

        if (!exist)
        {
            _logger.LogInformation("Подкатегория не найдена по Id: '{Id}'.", subCategoryId);
            throw new SubCategoryNotFoundByIdException(subCategoryId);
        }
    }


    private async Task<SubCategoryDto> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        if (dto != null) return dto;

        _logger.LogInformation("Подкатегория не найдена по Id: '{Id}'", id);
        throw new SubCategoryNotFoundByIdException(id);
    }


    private async Task CheckSubCategoryNameExistence(Guid categoryId, string subCategoryName,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Проверка подкатегории на совпадение по наименованию.");

        var exist =
            await _subCategoryRepository.DoesCategoryExistWhereAsync(
                s => s.CategoryId == categoryId && s.Name == subCategoryName, cancellationToken);

        if (exist)
        {
            _logger.LogInformation("Подкатегория '{Name}' уже существует в категории с Id: '{Id}'.",
                subCategoryName, categoryId);
            throw new SubCategoryAlreadyExistsException(subCategoryName);
        }
    }
}