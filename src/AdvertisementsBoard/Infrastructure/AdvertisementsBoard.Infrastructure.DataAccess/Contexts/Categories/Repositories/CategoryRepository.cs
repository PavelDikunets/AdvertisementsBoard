using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Domain.Categories;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;

/// <inheritdoc />
public class CategoryRepository : ICategoryRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<Category> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий категорий.</param>
    /// <param name="mapper">Маппер.</param>
    public CategoryRepository(IBaseDbRepository<Category> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAll()
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<CategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .ProjectTo<CategoryShortInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Category entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<CategoryUpdateDto> UpdateAsync(Category updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        var dto = _mapper.Map<CategoryUpdateDto>(updatedEntity);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(c => c.Id == id, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(c => c.Name == name, cancellationToken);
        return result;
    }
}