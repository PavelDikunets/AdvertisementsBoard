using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Domain.Categories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;

/// <inheritdoc />
public class CategoryService : ICategoryService
{
    private readonly ICategoryRepository _categoryRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryService" />
    /// </summary>
    /// <param name="categoryRepository">Репозиторий для работы с категориями.</param>
    public CategoryService(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    /// <inheritdoc />
    public async Task<CategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        var dto = new CategoryInfoDto
        {
            Name = entity.Name
        };
        return dto;
    }

    /// <inheritdoc />
    public async Task<CategoryDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _categoryRepository.GetAllAsync(cancellationToken);

        var dtos = entities.Select(c => new CategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToArray();

        return dtos;
    }


    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        var entity = new Category
        {
            Name = dto.Name
        };

        var id = await _categoryRepository.CreateAsync(entity, cancellationToken);

        return id;
    }


    /// <inheritdoc />
    public async Task<CategoryUpdateDto> UpdateByIdAsync(Guid id, CategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        entity.Name = dto.Name;

        await _categoryRepository.UpdateAsync(entity, cancellationToken);

        var updateDto = new CategoryUpdateDto
        {
            Name = entity.Name
        };

        return updateDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private async Task<Category> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Категория с идентификатором {id} не найдена.");
        return entity;
    }

    private async Task CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.CheckIfExistsByNameAsync(name, cancellationToken);
        if (exists) throw new AlreadyExistsException($"Категория с именем {name} уже существует.");
    }
}