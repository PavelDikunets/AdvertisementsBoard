using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;

/// <inheritdoc />
public class SubCategoryService : ISubCategoryService
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;
    private readonly ISubCategoryRepository _subCategoryRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryService" />
    /// </summary>
    /// <param name="subCategoryRepository">Репозиторий для работы с подкатегориями.</param>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    /// <param name="mapper">Маппер.</param>
    public SubCategoryService(ISubCategoryRepository subCategoryRepository, ICategoryService categoryService,
        IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _categoryService = categoryService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null) throw new NotFoundException($"Подкатегория с идентификатором {id} не найдена.");

        var infoDto = _mapper.Map<SubCategoryInfoDto>(dto);
        return infoDto;
    }

    /// <inheritdoc />
    public async Task<SubCategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _subCategoryRepository.GetAllAsync(cancellationToken);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        await _categoryService.TryFindByIdAsync(dto.CategoryId, cancellationToken);

        await CheckIfExistsByNameAsync(dto.Name, cancellationToken);

        var entity = _mapper.Map<SubCategory>(dto);

        var id = await _subCategoryRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryUpdateDto> UpdateByIdAsync(Guid id, SubCategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);

        await CheckIfExistsByNameAsync(updateDto.Name, cancellationToken);

        var currentDto = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        _mapper.Map(updateDto, currentDto);

        var entity = _mapper.Map<SubCategory>(currentDto);

        var updatedDto = await _subCategoryRepository.UpdateAsync(entity, cancellationToken);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);
        await _subCategoryRepository.DeleteByIdAsync(id, cancellationToken);
    }


    public async Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _subCategoryRepository.TryFindByIdAsync(id, cancellationToken);
        if (!exists) throw new NotFoundException($"Подкатегория с идентификатором {id} не найдена.");
    }

    private async Task CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var exists = await _subCategoryRepository.CheckIfExistsByNameAsync(name, cancellationToken);
        if (exists) throw new AlreadyExistsException($"Подкатегория с именем {name} уже существует.");
    }
}