using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;
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
        var subCategory = await TryGetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<SubCategoryDto>(subCategory);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<SubCategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listSubCategories = await _repository.GetAll()
            .ProjectTo<SubCategoryShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listSubCategories;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategoryDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<SubCategory>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> UpdateAsync(SubCategoryDto dto, CancellationToken cancellationToken)
    {
        var subCategory = _mapper.Map<SubCategory>(dto);

        await _repository.UpdateAsync(subCategory, cancellationToken);

        var updatedDto = _mapper.Map<SubCategoryInfoDto>(subCategory);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(subCategory, cancellationToken);
    }


    public async Task<SubCategoryDto> FindWhereAsync(Expression<Func<SubCategory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var subCategory = await _repository.GetAllFiltered(predicate)
            .Include(s => s.Category)
            .ProjectTo<SubCategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (subCategory == null) throw new SubCategoryNotFoundException();

        return subCategory;
    }

    /// <inheritdoc />
    public async Task<bool> DoesSubCategoryExistWhereAsync(Expression<Func<SubCategory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        return await _repository.FindAnyAsync(predicate, cancellationToken);
    }


    private async Task<SubCategory> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await _repository.GetByIdAsync(id, cancellationToken);

        if (subCategory == null) throw new SubCategoryNotFoundException(id);

        return subCategory;
    }
}