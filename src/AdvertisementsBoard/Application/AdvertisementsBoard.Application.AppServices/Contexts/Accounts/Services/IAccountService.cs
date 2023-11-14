using AdvertisementsBoard.Contracts.Accounts;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Services;

/// <summary>
///     Сервис для работы с аккаунтами.
/// </summary>
public interface IAccountService
{
    /// <summary>
    ///     Получить аккаунт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией об аккаунте.</returns>
    Task<AccountAdminDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить аккаунт по идентификатору пользователя.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией об аккаунте.</returns>
    Task<AccountInfoDto> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить список аккаунтов.
    /// </summary>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="isBlocked">Признак блокировки аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Список аккаунтов с краткой информацией.</returns>
    Task<List<AccountShortInfoDto>> GetAllAsync(int pageSize, int pageNumber,
        bool? isBlocked, CancellationToken cancellationToken);

    /// <summary>
    ///     Пройти аутентификацию.
    /// </summary>
    /// <param name="signInDto">Модель аутентификации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Jwt токен.</returns>
    Task<string> SignInAsync(AccountSignInDto signInDto, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать аккаунт.
    /// </summary>
    /// <param name="dto"></param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель созданного аккаунта.</returns>
    Task<AccountCreatedDto> SignUpAsync(AccountCreateDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить аккаунт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Изменить пароль аккаунта.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    /// <param name="accountDto">Модель изменения пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task ChangePasswordAsync(Guid userId, AccountPasswordEditDto accountDto, CancellationToken cancellationToken);

    /// <summary>
    ///     Заблокировать аккаунт.
    /// </summary>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="dto">Модель блокировки аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель блокировки пользователя.</returns>
    /// >
    Task<AccountBlockStatusDto> BlockByIdAsync(Guid id, AccountBlockStatusDto dto,
        CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить признак блокировки аккаунта.
    /// </summary>
    /// <param name="userId">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>True, если аккаунт заблокирован, false в противном случае.</returns>
    Task<bool> IsAccountBlocked(Guid userId, CancellationToken cancellationToken);
}