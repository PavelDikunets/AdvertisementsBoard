using System.Linq.Expressions;
using AdvertisementsBoard.Contracts.Accounts;
using AdvertisementsBoard.Domain.Accounts;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;

/// <summary>
///     Репозиторий для работы с аккаунтами.
/// </summary>
public interface IAccountRepository
{
    /// <summary>
    ///     Получить аккаунт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель аккаунта.</returns>
    Task<AccountInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать аккаунт.
    /// </summary>
    /// <param name="dto">Модель аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<AccountCreatedDto> CreateAsync(AccountDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все аккаунты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize"></param>
    /// <param name="pageNumber"></param>
    /// <param name="isBlocked"></param>
    /// <returns>Список аккаунтов с краткой информацией.</returns>
    Task<List<AccountShortInfoDto>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber,
        bool? isBlocked);

    /// <summary>
    ///     Обновить аккаунт.
    /// </summary>
    /// <param name="dto">Модель аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной информацией аккаунта.</returns>
    Task<AccountInfoDto> UpdateAsync(AccountDto dto, CancellationToken cancellationToken);

    /// <summary>
    ///     Удалить аккаунт по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Проверить, существует ли аккаунт с указанным фильтром.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Возвращает true, если аккаунт существует, и false в противном случае.</returns>
    Task<bool> DoesAccountExistWhereAsync(Expression<Func<Account, bool>> filter, CancellationToken cancellationToken);

    /// <summary>
    ///     Найти аккаунт по фильтру.
    /// </summary>
    /// <param name="filter">Фильтр.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель аккаунта.</returns>
    Task<AccountDto> FindWhereAsync(Expression<Func<Account, bool>> filter, CancellationToken cancellationToken);
}