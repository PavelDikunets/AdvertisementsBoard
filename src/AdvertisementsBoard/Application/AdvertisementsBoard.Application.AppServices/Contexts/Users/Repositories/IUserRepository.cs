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
    /// <returns>Массив моделей пользователей с краткой информацией.</returns>
    Task<UserShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать пользователя.
    /// </summary>
    /// <param name="entity">Сущность пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<Guid> CreateAsync(User entity, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="updatedEntity">Обновленная сущность пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным пользователем.</returns>
    Task<UserInfoDto> UpdateAsync(User updatedEntity, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли пользователь с указанным идентификатором.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если пользователь существует, и false в противном случае.</returns>
    Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли пользователь с указанным адресом электронной почты.
    /// </summary>
    /// <param name="email">Адрес электронной почты.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если адрес электронной почты существует, и false в противном случае.</returns>
    Task<bool> CheckIfExistsByEmailAsync(string email, CancellationToken cancellationToken);
}