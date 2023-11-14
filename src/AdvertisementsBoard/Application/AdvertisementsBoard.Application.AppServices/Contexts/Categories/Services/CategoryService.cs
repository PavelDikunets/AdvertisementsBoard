using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Domain.Categories;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc />
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IMapper _mapper;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryService" />
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<CategoryInfoDto>(subCategory);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listCategories = await _categoryRepository.GetAllAsync(cancellationToken);
        var listDto = _mapper.Map<List<CategoryShortInfoDto>>(listCategories);
        return listDto;
    }

    /// <inheritdoc />
    public async Task<CategoryShortInfoDto> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        await DoesCategoryExistByNameAsync(dto.Name, cancellationToken);

        var entity = _mapper.Map<Category>(dto);

        var category = await _categoryRepository.CreateAsync(entity, cancellationToken);

        var createdDto = _mapper.Map<CategoryShortInfoDto>(category);
        return createdDto;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> UpdateByIdAsync(Guid id, CategoryEditDto categoryDto,
        CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.FindWhereAsync(c => c.Id == id, cancellationToken);

        await DoesCategoryExistByNameAsync(categoryDto.Name, cancellationToken);

        _mapper.Map(categoryDto, category);

        var updatedCategory = await _categoryRepository.UpdateAsync(category, cancellationToken);

        var updatedDto = _mapper.Map<CategoryInfoDto>(updatedCategory);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    private async Task DoesCategoryExistByNameAsync(string categoryName, CancellationToken cancellationToken)
    {
        var exist = await _categoryRepository.DoesCategoryExistWhereAsync(s => s.Name == categoryName,
            cancellationToken);

        if (exist) throw new CategoryAlreadyExistsException(categoryName);
    }
}