using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Domain.Categories;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;

/// <summary>
///     Репозиторий категорий.
/// </summary>
public class CategoryRepository : ICategoryRepository
{
    private readonly IBaseDbRepository<Category> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий категорий.</param>
    public CategoryRepository(IBaseDbRepository<Category> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Category> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToArrayAsync(cancellationToken);
        return entities;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Category entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Category updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> CheckIfExistsByNameAsync(string name, CancellationToken cancellationToken)
    {
        return await _repository.GetAll().AnyAsync(c => c.Name == name, cancellationToken);
    }
}