using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.SubCategories.Repositories;

/// <inheritdoc />
public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<SubCategory> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий подкатегорий.</param>
    /// <param name="mapper">Маппер.</param>
    public SubCategoryRepository(IBaseDbRepository<SubCategory> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<SubCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<SubCategoryDto>(entity);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<SubCategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .ProjectTo<SubCategoryShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategoryCreateDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SubCategory>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryUpdatedDto> UpdateAsync(SubCategoryDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SubCategory>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        return _mapper.Map<SubCategoryUpdatedDto>(entity);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }


    /// <inheritdoc />
    public async Task<bool> DoesCategoryExistWhereAsync(Expression<Func<SubCategory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _repository.FindAnyAsync(predicate, cancellationToken);
    }

    public async Task<SubCategoryDto> GetWhereAsync(Expression<Func<SubCategory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAllFiltered(predicate)
            .ProjectTo<SubCategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return dto;
    }
}