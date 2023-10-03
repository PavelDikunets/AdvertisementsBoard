using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;

/// <inheritdoc />
public class SubCategoryService : ISubCategoryService
{
    private readonly ICategoryService _categoryService;
    private readonly ISubCategoryRepository _subCategoryRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryService" />
    /// </summary>
    /// <param name="subCategoryRepository">Репозиторий для работы с подкатегориями.</param>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    public SubCategoryService(ISubCategoryRepository subCategoryRepository, ICategoryService categoryService)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryService = categoryService;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        var dto = new SubCategoryInfoDto
        {
            Name = entity.Name
        };
        return dto;
    }

    /// <inheritdoc />
    public async Task<SubCategoryDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _subCategoryRepository.GetAllAsync(cancellationToken);

        var dtos = entities.Select(c => new SubCategoryDto
        {
            Id = c.Id,
            Name = c.Name
        }).ToArray();

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateByCategoryIdAsync(Guid categoryId, SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        await _categoryService.GetByIdAsync(categoryId, cancellationToken);

        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        var entity = new SubCategory
        {
            CategoryId = categoryId,
            Name = dto.Name
        };

        var id = await _subCategoryRepository.CreateAsync(entity, cancellationToken);

        return id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryUpdateDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        entity.Name = dto.Name;

        await _subCategoryRepository.UpdateAsync(entity, cancellationToken);

        var updateDto = new SubCategoryUpdateDto
        {
            Name = entity.Name
        };

        return updateDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subCategoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private async Task<SubCategory> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Подкатегория с идентификатором {id} не найдена.");
        return entity;
    }

    private async Task CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var exists = await _subCategoryRepository.CheckIfExistsByNameAsync(name, cancellationToken);
        if (exists) throw new AlreadyExistsException($"Подкатегория с именем {name} уже существует.");
    }
}