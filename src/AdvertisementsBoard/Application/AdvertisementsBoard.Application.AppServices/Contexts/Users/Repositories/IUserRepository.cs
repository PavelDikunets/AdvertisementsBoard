using System.Linq.Expressions;
using AdvertisementsBoard.Contracts.Users;
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
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей с краткой информацией.</returns>
    Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="dto">Модель пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной информацией пользователя.</returns>
    Task<UserUpdatedDto> UpdateAsync(UserDto dto, CancellationToken cancellationToken);

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
    /// <returns>Модель пользователя.</returns>
    Task<UserDto> FindWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken);
}