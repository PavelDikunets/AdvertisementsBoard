using System.Linq.Expressions;
using AdvertisementsBoard.Domain.Users;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;

/// <summary>
///     Репозиторий для работы с пользователями.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    ///     Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя.</returns>
    Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей с краткой информацией.</returns>
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="user">Пользователь.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Обновленный пользователь.</returns>
    Task<User> UpdateAsync(User user, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли пользователь с указанным фильтром.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если пользователь существует, и false в противном случае.</returns>
    Task<bool> DoesUserExistWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken);

    /// <summary>
    ///     Найти пользователя по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Пользователь.</returns>
    Task<User> FindWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken);
}