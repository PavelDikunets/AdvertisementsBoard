using AdvertisementsBoard.Application.AppServices.Contexts.Categories.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Contracts.Categories;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc />
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryService" />
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="logger">Логирование сервиса <see cref="CategoryService" />.</param>
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение категории по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        var infoDto = _mapper.Map<CategoryInfoDto>(dto);

        _logger.LogInformation("Категория успешно получена по Id: '{Id}'.", id);

        return infoDto;
    }

    /// <inheritdoc />
    public async Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение коллекции категорий.");

        var dtos = await _categoryRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("Коллекция категорий успешно получена.");

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание категории '{Name}'.", dto.Name);

        await EnsureCategoryExistsByNameAsync(dto.Name, cancellationToken);

        var id = await _categoryRepository.CreateAsync(dto, cancellationToken);

        _logger.LogInformation("Категория '{Name}' Id: '{Id}' успешно создана.", dto.Name, id);

        return id;
    }

    /// <inheritdoc />
    public async Task<CategoryUpdatedDto> UpdateByIdAsync(Guid id, CategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление категории по Id: '{Id}'.", id);

        await EnsureCategoryExistsByNameAsync(dto.Name, cancellationToken);

        var currentDto = await _categoryRepository.GetWhereAsync(c => c.Id == id, cancellationToken);

        if (currentDto == null)
        {
            _logger.LogInformation("Категория не найдена по Id: '{Id}'.", id);
            throw new CategoryNotFoundByIdException(id);
        }

        var currentCategoryName = currentDto.Name;

        _mapper.Map(dto, currentDto);

        var updatedDto = await _categoryRepository.UpdateAsync(currentDto, cancellationToken);

        _logger.LogInformation("Подкатегория '{Name}' успешно обновлена на '{updatedName}' Id: '{Id}'.",
            currentCategoryName, updatedDto.Name, id);

        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление категории по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        await _categoryRepository.DeleteByIdAsync(dto.Id, cancellationToken);

        _logger.LogWarning("Категория удалена по Id: '{Id}'.", id);
    }

    /// <inheritdoc />
    public async Task EnsureCategoryExistsByIdAsync(Guid categoryId,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос существования категории по Id: '{Id}'.", categoryId);

        var exist = await _categoryRepository.DoesCategoryExistWhereAsync(s => s.Id == categoryId,
            cancellationToken);
        if (!exist)
        {
            _logger.LogInformation("Категория не найдена по Id: '{Id}'.", categoryId);
            throw new CategoryNotFoundByIdException(categoryId);
        }
    }


    private async Task<CategoryDto> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (dto != null) return dto;

        _logger.LogInformation("Подкатегория не найдена по Id: '{Id}'", id);
        throw new CategoryNotFoundByIdException(id);
    }


    private async Task EnsureCategoryExistsByNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        var exist = await _categoryRepository.DoesCategoryExistWhereAsync(s => s.Name == categoryName,
            cancellationToken);
        if (exist)
        {
            _logger.LogInformation("Категория '{Name}' уже существует.", categoryName);
            throw new CategoryAlreadyExistsException(categoryName);
        }
    }
}