using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;
using AdvertisementsBoard.Contracts.Categories;
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
        return listCategories;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        await DoesCategoryExistByNameAsync(dto.Name, cancellationToken);

        var id = await _categoryRepository.CreateAsync(dto, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> UpdateByIdAsync(Guid id, CategoryEditDto editDto,
        CancellationToken cancellationToken)
    {
        var currentCategory = await _categoryRepository.FindWhereAsync(c => c.Id == id, cancellationToken);

        await DoesCategoryExistByNameAsync(editDto.Name, cancellationToken);

        _mapper.Map(editDto, currentCategory);

        var updatedDto = await _categoryRepository.UpdateAsync(currentCategory, cancellationToken);
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