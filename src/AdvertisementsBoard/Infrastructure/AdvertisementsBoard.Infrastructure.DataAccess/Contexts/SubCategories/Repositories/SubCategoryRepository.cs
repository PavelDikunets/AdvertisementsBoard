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
        var listOfSubCategories = await _repository.FindWhereAsync(s => s.CategoryId == categoryId)
            .ToListAsync(cancellationToken);

        return listOfSubCategories;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(SubCategory entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<SubCategory> UpdateAsync(SubCategory entity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(entity, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(subCategory, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DoesSubCategoryExistWhereAsync(Expression<Func<SubCategory, bool>> filter,
        CancellationToken cancellationToken)
    {
        var subCategoryExists = await _repository.FindAnyAsync(filter, cancellationToken);
        return subCategoryExists;
    }

    public async Task<SubCategory> FindWhereAsync(Expression<Func<SubCategory, bool>> filter,
        CancellationToken cancellationToken)
    {
        var subCategory = await _repository.FindWhereAsync(filter)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (subCategory == null) throw new SubCategoryNotFoundException();

        return subCategory;
    }


    private async Task<SubCategory> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await _repository.GetByIdAsync(id, cancellationToken);

        if (subCategory == null) throw new SubCategoryNotFoundException(id);
        return subCategory;
    }
}