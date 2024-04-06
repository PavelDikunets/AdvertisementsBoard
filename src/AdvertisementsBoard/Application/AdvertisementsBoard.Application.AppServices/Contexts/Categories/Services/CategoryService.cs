using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Domain.Categories;
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
    /// <param name="logger">Логгер.</param>
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper, ILogger<CategoryService> logger)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var categoryEntity = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        var categoryDto = _mapper.Map<CategoryInfoDto>(categoryEntity);
        return categoryDto;
    }

    /// <inheritdoc />
    public async Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var categoryEntities = await _categoryRepository.GetAllAsync(cancellationToken);

        var categoryDtos = _mapper.Map<List<CategoryShortInfoDto>>(categoryEntities);
        return categoryDtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto createDto,
        CancellationToken cancellationToken)
    {
        var categoryExist =
            await _categoryRepository.DoesCategoryExistWhereAsync(c => c.Name == createDto.Name, cancellationToken);

        if (categoryExist) throw new CategoryAlreadyExistsException(createDto.Name);

        var newCategoryEntity = _mapper.Map<Category>(createDto);

        var createdCategoryId = await _categoryRepository.CreateAsync(newCategoryEntity, cancellationToken);
        return createdCategoryId;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> UpdateByIdAsync(Guid id, CategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var categoryEntity = await _categoryRepository.FindWhereAsync(c => c.Id == id, cancellationToken);

        _logger.LogInformation("Запрос обновления категории: '{Category}.", categoryEntity.Name);

        if (categoryEntity.Name == updateDto.Name) throw new CategoryAlreadyExistsException(updateDto.Name);

        _mapper.Map(updateDto, categoryEntity);

        var updatedCategoryEntity = await _categoryRepository.UpdateAsync(categoryEntity, cancellationToken);

        var updatedCategoryDto = _mapper.Map<CategoryInfoDto>(updatedCategoryEntity);
        return updatedCategoryDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    public async Task DoesCategoryExistAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var categoryExists =
            await _categoryRepository.DoesCategoryExistWhereAsync(s => s.Id == categoryId, cancellationToken);
        if (!categoryExists) throw new CategoryNotFoundException(categoryId);
    }
}