using AdvertisementsBoard.Contracts.Users;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <summary>
/// Сервис для работы с пользователями.
/// </summary>
public interface IUserService
{
    /// <summary>
    ///     Получить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель информации о пользователе <see cref="UserInfoDto" />.</returns>
    Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив моделей информации о пользователях <see cref="UserInfoDto" />.</returns>
    Task<UserInfoDto[]> GetAllAsync(CancellationToken cancellationToken);

    /// <summary>
    ///     Создать пользователя.
    /// </summary>
    /// <param name="dto">Модель создания пользователя <see cref="UserCreateDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя <see cref="Guid" />.</returns>
    Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="dto">Модель обновления пользователя. <see cref="UserUpdateDto" /></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель информации о пользователе <see cref="UserInfoDto" />.</returns>
    Task<UserInfoDto> UpdateByIdAsync(Guid id, UserUpdateDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить пользователя по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);
}