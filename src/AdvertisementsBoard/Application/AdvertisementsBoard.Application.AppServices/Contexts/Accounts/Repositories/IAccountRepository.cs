using System.Linq.Expressions;
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
    /// <returns>Сущность аккаунта.</returns>
    Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    /// <summary>
    ///     Создать аккаунт.
    /// </summary>
    /// <param name="account">Сущность аккаунта</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор созданного пользователя.</returns>
    Task<Account> CreateAsync(Account account, CancellationToken cancellationToken);

    /// <summary>
    ///     Получить все аккаунты.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageNumber">Номер страницы</param>
    /// <param name="isBlocked">Признак блокировки.</param>
    /// <returns>Список аккаунтов с краткой информацией.</returns>
    Task<List<Account>> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber,
        bool? isBlocked);

    /// <summary>
    ///     Обновить аккаунт.
    /// </summary>
    /// <param name="account">Сущность аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с обновленной информацией аккаунта.</returns>
    Task<Account> UpdateAsync(Account account, CancellationToken cancellationToken);

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
    Task<Account> FindWhereAsync(Expression<Func<Account, bool>> filter, CancellationToken cancellationToken);
}