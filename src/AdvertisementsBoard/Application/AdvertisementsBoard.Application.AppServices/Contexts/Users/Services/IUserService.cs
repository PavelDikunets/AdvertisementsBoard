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
    Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив моделей пользователей с краткой информацией.</returns>
    Task<UserShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать пользователя.
    /// </summary>
    /// <param name="dto">Модель создания пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="updatedDto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленным пользователем.</returns>
    Task<UserInfoDto> UpdateByIdAsync(Guid id, UserUpdateDto updatedDto,
        CancellationToken cancellationToken);

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
    Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken);
}