using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;
using AdvertisementsBoard.Domain.SubCategories;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.SubCategories.Repositories;

/// <inheritdoc />
public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly IBaseDbRepository<SubCategory> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий подкатегорий.</param>
    public SubCategoryRepository(IBaseDbRepository<SubCategory> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<SubCategory> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await TryGetByIdAsync(id, cancellationToken);

        return subCategory;
    }

    /// <inheritdoc />
    public async Task<List<SubCategory>> GetAllAsync(Guid categoryId, CancellationToken cancellationToken)
    {
        var listSubCategories = await _repository.GetAll()
            .Where(a => a.CategoryId == categoryId)
            .ToListAsync(cancellationToken);

        return listSubCategories;
    }

    /// <inheritdoc />
    public async Task<SubCategory> CreateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(subCategory, cancellationToken);
        return subCategory;
    }

    /// <inheritdoc />
    public async Task<SubCategory> UpdateAsync(SubCategory subCategory, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(subCategory, cancellationToken);
        return subCategory;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(subCategory, cancellationToken);
    }


    public async Task<SubCategory> FindWhereAsync(Expression<Func<SubCategory, bool>> predicate,
        CancellationToken cancellationToken)
    {
        var subCategory = await _repository.GetAllFiltered(predicate)
            .AsNoTracking()
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