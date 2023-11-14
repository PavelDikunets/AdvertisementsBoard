using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;
using AdvertisementsBoard.Domain.Categories;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Categories.Repositories;

/// <inheritdoc />
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
        var category = await TryGetByIdAsync(id, cancellationToken);
        return category;
    }

    /// <inheritdoc />
    public async Task<List<Category>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listCategories = await _repository.GetAll().ToListAsync(cancellationToken);
        return listCategories;
    }

    /// <inheritdoc />
    public async Task<Category> CreateAsync(Category category, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(category, cancellationToken);
        return category;
    }

    /// <inheritdoc />
    public async Task<Category> UpdateAsync(Category category, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(category, cancellationToken);
        return category;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(category, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DoesCategoryExistWhereAsync(Expression<Func<Category, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.FindAnyAsync(filter, cancellationToken);
        return exists;
    }

    public async Task<Category> FindWhereAsync(Expression<Func<Category, bool>> filter,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .AsSplitQuery()
            .FirstOrDefaultAsync(cancellationToken);

        if (category == null) throw new CategoryNotFoundException();
        return category;
    }

    private async Task<Category> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(id, cancellationToken);

        if (category == null) throw new CategoryNotFoundException(id);
        return category;
    }
}