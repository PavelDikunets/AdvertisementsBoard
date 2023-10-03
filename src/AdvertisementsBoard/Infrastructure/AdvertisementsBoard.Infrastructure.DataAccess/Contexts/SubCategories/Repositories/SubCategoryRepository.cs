using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Domain.SubCategories;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.SubCategories.Repositories;

/// <inheritdoc />
public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly IBaseDbRepository<SubCategory> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий подкатегорий.</param>
    public SubCategoryRepository(IBaseDbRepository<SubCategory> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<SubCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<SubCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToArrayAsync(cancellationToken);
        return entities;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategory entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(SubCategory updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
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
        return await _repository.GetAll().AnyAsync(c => c.Name == name, cancellationToken);
    }
}