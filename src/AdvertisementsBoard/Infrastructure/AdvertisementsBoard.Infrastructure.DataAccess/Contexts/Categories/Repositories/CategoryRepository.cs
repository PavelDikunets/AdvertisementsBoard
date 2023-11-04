using System.Linq.Expressions;
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
            .Where(c => c.Id == id)
            .Include(s => s.SubCategories)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<List<CategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .ProjectTo<CategoryShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Category>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<CategoryUpdatedDto> UpdateAsync(CategoryDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Category>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        var updatedDto = _mapper.Map<CategoryUpdatedDto>(entity);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DoesCategoryExistWhereAsync(Expression<Func<Category, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.FindAnyAsync(filter, cancellationToken);
        return exists;
    }

    public async Task<CategoryDto> GetWhereAsync(Expression<Func<Category, bool>> filter,
        CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAllFiltered(filter)
            .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return dto;
    }
}