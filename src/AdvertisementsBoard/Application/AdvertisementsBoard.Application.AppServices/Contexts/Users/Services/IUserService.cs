using AdvertisementsBoard.Contracts.Users;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <summary>
///     Сервис для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией о пользователе.</returns>
    Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей с краткой информацией.</returns>
    Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="updateDto">Модель редактирования пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя.</returns>
    Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserUpdateDto updateDto, CancellationToken cancellationToken);

    /// <summary>
    ///     Назначить роль пользователю.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="roleDto">Модель с ролью пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с ролью пользователя.</returns>
    Task<UserRoleDto> SetRoleByIdAsync(Guid id, UserRoleDto roleDto, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверка пользователя на существование по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пользовать сущесвтует, false в противном случае</returns>
    /// >
    Task DoesUserExistByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверят, есть ли у пользователя разрешение на изменения.
    /// </summary>
    /// <param name="currentUserid">Идентификатор текущего пользователя.</param>
    /// <param name="otherSourceUserId">Идентификатор пользователя, полученный из другого источника.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если пользователь является автором объявления, false в противном случае.</returns>
    Task CheckUserPermissionAsync(Guid currentUserid, Guid otherSourceUserId, CancellationToken cancellationToken);
}