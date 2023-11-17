using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Domain.Accounts;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Accounts.Repositories;

/// <inheritdoc />
public class AccountRepository : IAccountRepository
{
    private readonly IBaseDbRepository<Account> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AccountRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий аккаунтов.</param>
    public AccountRepository(IBaseDbRepository<Account> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Account> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await TryGetByIdAsync(id, cancellationToken);
        return account;
    }

    /// <inheritdoc />
    public async Task<List<Account>> GetAllAsync(CancellationToken cancellationToken,
        int pageNumber, int pageSize, bool isBlocked)
    {
        var listOfAccounts = await _repository.GetAllFiltered(a => true)
            .Where(s => s.IsBlocked == isBlocked)
            .OrderBy(a => a.Email)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ToListAsync(cancellationToken);

        return listOfAccounts;
    }

    /// <inheritdoc />
    public async Task<Account> CreateAsync(Account account, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(account, cancellationToken);
        return account;
    }

    /// <inheritdoc />
    public async Task<Account> UpdateAsync(Account account, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(account, cancellationToken);
        return account;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await TryGetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(account, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> DoesAccountExistWhereAsync(Expression<Func<Account, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exist = await _repository.FindAnyAsync(filter, cancellationToken);
        return exist;
    }

    public async Task<Account> FindWhereAsync(Expression<Func<Account, bool>> filter,
        CancellationToken cancellationToken)
    {
        var account = await _repository.GetAllFiltered(filter)
            .FirstOrDefaultAsync(cancellationToken);

        if (account == null) throw new AccountNotFoundException();
        return account;
    }


    private async Task<Account> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);

        if (account == null) throw new AccountNotFoundException();
        return account;
    }
}