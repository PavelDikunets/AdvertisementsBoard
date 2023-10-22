using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
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
        var dto = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null) throw new NotFoundException($"Категория с идентификатором {id} не найдена.");

        var infoDto = _mapper.Map<CategoryInfoDto>(dto);
        return infoDto;
    }

    /// <inheritdoc />
    public async Task<CategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _categoryRepository.GetAllAsync(cancellationToken);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        var entity = _mapper.Map<Category>(dto);

        var id = await _categoryRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<CategoryUpdateDto> UpdateByIdAsync(Guid id, CategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);

        await CheckIfExistsByNameAsync(updateDto.Name, cancellationToken);

        var currentDto = await _categoryRepository.GetByIdAsync(id, cancellationToken);

        _mapper.Map(updateDto, currentDto);

        var entity = _mapper.Map<Category>(currentDto);

        var updatedDto = await _categoryRepository.UpdateAsync(entity, cancellationToken);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);
        await _categoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    /// <inheritdoc />
    public async Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.TryFindByIdAsync(id, cancellationToken);
        if (!exists) throw new NotFoundException($"Категория с идентификатором {id} не найдена.");
    }

    private async Task CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var exists = await _categoryRepository.CheckIfExistsByNameAsync(name, cancellationToken);
        if (exists) throw new AlreadyExistsException($"Категория с именем {name} уже существует.");
    }
}