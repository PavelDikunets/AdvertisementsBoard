using System.Linq.Expressions;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;

/// <summary>
///     Репозиторий для работы с подкатегориями.
/// </summary>
public interface ISubCategoryRepository
{
    /// <summary>
    ///     Получить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории.</returns>
    Task<SubCategoryDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все подкатегории в категории.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <returns>Список подкатегорий в указанной категории.</returns>
    Task<List<SubCategoryShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать подкатегорию.
    /// </summary>
    /// <param name="dto">Модель создания подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    Task<Guid> CreateAsync(SubCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="dto">Модель подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной подкатегорией.</returns>
    Task<SubCategoryInfoDto> UpdateAsync(SubCategoryDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли подкатегория с указанным фильтром.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если категория существует, и false в противном случае.</returns>
    Task<bool> DoesSubCategoryExistWhereAsync(Expression<Func<SubCategory, bool>> filter,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Получить подкатегорию по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории.</returns>
    Task<SubCategoryDto> FindWhereAsync(Expression<Func<SubCategory, bool>> filter,
        CancellationToken cancellationToken);
}