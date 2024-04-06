using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;
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
    ///     Инициализирует экземпляр <see cref="SubCategoryService" />.
    /// </summary>
    /// <param name="subCategoryRepository">Репозиторий для работы с подкатегориями.</param>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="logger">Логгер.</param>
    public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper mapper,
        ICategoryService categoryService, ILogger<SubCategoryService> logger)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategoryEntity = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        var subCategoryDto = _mapper.Map<SubCategoryInfoDto>(subCategoryEntity);
        return subCategoryDto;
    }

    /// <inheritdoc />
    public async Task<List<SubCategoryShortInfoDto>> GetAllAsync(Guid id, CancellationToken cancellationToken)
    {
        var listOfSubCategoryEntities = await _subCategoryRepository.GetAllAsync(id, cancellationToken);

        var subCategoryDtos = _mapper.Map<List<SubCategoryShortInfoDto>>(listOfSubCategoryEntities);
        return subCategoryDtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Guid categoryId, SubCategoryCreateDto categoryDto,
        CancellationToken cancellationToken)
    {
        await _categoryService.DoesCategoryExistAsync(categoryId, cancellationToken);

        var subCategoryExists =
            await _subCategoryRepository.DoesSubCategoryExistWhereAsync(s => s.Name == categoryDto.Name,
                cancellationToken);

        if (subCategoryExists) throw new SubCategoryAlreadyExistsException(categoryDto.Name);

        var newSubCategoryEntity = _mapper.Map<SubCategory>(categoryDto);
        newSubCategoryEntity.CategoryId = categoryId;

        var createdSubCategoryId =
            await _subCategoryRepository.CreateAsync(newSubCategoryEntity, cancellationToken);

        return createdSubCategoryId;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var subCategoryEntity =
            await _subCategoryRepository.FindWhereAsync(s => s.Id == id, cancellationToken);

        _logger.LogInformation("Запрос обновления подкатегории: '{SubCategory}.", subCategoryEntity.Name);

        var subCategoryExistsByName =
            await _subCategoryRepository.DoesSubCategoryExistWhereAsync(s => s.Name == updateDto.Name,
                cancellationToken);
        if (subCategoryExistsByName) throw new SubCategoryAlreadyExistsException(updateDto.Name);

        _mapper.Map(updateDto, subCategoryEntity);

        var updatedSubCategoryEntity =
            await _subCategoryRepository.UpdateAsync(subCategoryEntity, cancellationToken);

        var updatedSubCategoryDto = _mapper.Map<SubCategoryInfoDto>(updatedSubCategoryEntity);
        return updatedSubCategoryDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subCategoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DoesSubCategoryExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategoryExists =
            await _subCategoryRepository.DoesSubCategoryExistWhereAsync(s => s.Id == id, cancellationToken);

        if (!subCategoryExists) throw new SubCategoryNotFoundException(id);
    }
}