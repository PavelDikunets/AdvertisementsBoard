using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Contracts.Accounts;
using AdvertisementsBoard.Domain.Accounts;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Accounts.Repositories;

/// <inheritdoc />
public class AccountRepository : IAccountRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<Account> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AccountRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий аккаунтов.</param>
    /// <param name="mapper">Маппер.</param>
    public AccountRepository(IBaseDbRepository<Account> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AccountInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await TryGetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AccountInfoDto>(account);
        return dto;
    }


    /// <inheritdoc />
    public async Task<List<AccountShortInfoDto>> GetAllAsync(CancellationToken cancellationToken,
        int pageNumber, int pageSize, bool? isBlocked)
    {
        var query = _repository.GetAllFiltered(a => true);

        if (isBlocked != null) query = query.Where(s => s.IsBlocked == isBlocked);

        var listAccounts = await query.OrderBy(a => a.Email)
            .Skip(pageNumber * pageSize)
            .Take(pageSize)
            .ProjectTo<AccountShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listAccounts;
    }

    /// <inheritdoc />
    public async Task<AccountCreatedDto> CreateAsync(AccountDto dto, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(dto);

        await _repository.AddAsync(account, cancellationToken);

        var createdDto = _mapper.Map<AccountCreatedDto>(account);

        return createdDto;
    }

    /// <inheritdoc />
    public async Task<AccountInfoDto> UpdateAsync(AccountDto dto, CancellationToken cancellationToken)
    {
        var account = _mapper.Map<Account>(dto);

        await _repository.UpdateAsync(account, cancellationToken);

        var infoDto = _mapper.Map<AccountInfoDto>(dto);

        return infoDto;
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

    /// <inheritdoc />
    public async Task<AccountDto> FindWhereAsync(Expression<Func<Account, bool>> filter,
        CancellationToken cancellationToken)
    {
        var account = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .Include(a => a.User)
            .ProjectTo<AccountDto>(_mapper.ConfigurationProvider)
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