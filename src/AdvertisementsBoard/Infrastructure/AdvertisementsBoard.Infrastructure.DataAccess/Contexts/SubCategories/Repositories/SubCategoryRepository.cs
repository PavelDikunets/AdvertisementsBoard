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
    ///     Инициализирует экземпляр <see cref="SubCategoryRepository" />
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
        var dto = await _repository.GetAll()
            .ProjectTo<SubCategoryDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<SubCategoryShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .ProjectTo<SubCategoryShortInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategory entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<SubCategoryUpdateDto> UpdateAsync(SubCategory updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        var dto = _mapper.Map<SubCategoryUpdateDto>(updatedEntity);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(c => c.Name == name, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(a => a.Id == id, cancellationToken);
        return result;
    }
}