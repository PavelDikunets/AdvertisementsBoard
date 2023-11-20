using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Domain.Users;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<User> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserRepository" />.
    /// </summary>
    /// <param name="repository">Репозиторий пользователей.</param>
    /// <param name="mapper">Маппер.</param>
    public UserRepository(IBaseDbRepository<User> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await TryGetByIdAsync(id, cancellationToken);
        return user;
    }

    /// <inheritdoc />
    public async Task<List<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listUsers = await _repository.GetAll()
            .AsNoTracking()
            .ToListAsync(cancellationToken);

        return listUsers;
    }

    /// <inheritdoc />
    public async Task<User> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(user, cancellationToken);
        return user;
    }

    public async Task<bool> DoesUserExistWhereAsync(Expression<Func<User, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exist = await _repository.FindAnyAsync(filter, cancellationToken);
        return exist;
    }

    public async Task<User> FindWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken)
    {
        var user = await _repository.FindWhereAsync(filter)
            .AsNoTracking()
            .FirstOrDefaultAsync(cancellationToken);

        if (user == null) throw new UserNotFoundException();
        return user;
    }


    private async Task<User> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByIdAsync(id, cancellationToken);

        if (user == null) throw new UserNotFoundException(id);
        return user;
    }
}