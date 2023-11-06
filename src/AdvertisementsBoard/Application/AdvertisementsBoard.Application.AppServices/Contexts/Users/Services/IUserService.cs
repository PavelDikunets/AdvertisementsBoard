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
    /// <returns>Модель с информацией о пользователе <see cref="UserInfoDto" />.</returns>
    Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список пользователей с краткой информацией <see cref="UserShortInfoDto" />.</returns>
    Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать пользователя.
    /// </summary>
    /// <param name="dto">Модель создания пользователя <see cref="UserCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="updatedDto">Модель обновления пользователя <see cref="UserUpdateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной информацией о пользователе <see cref="UserUpdatedDto" />.</returns>
    Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserUpdateDto updatedDto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверка пользователя на существование по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">окен отмены операции.</param>
    public Task EnsureUserExistsByIdAsync(Guid id, CancellationToken cancellationToken);
}