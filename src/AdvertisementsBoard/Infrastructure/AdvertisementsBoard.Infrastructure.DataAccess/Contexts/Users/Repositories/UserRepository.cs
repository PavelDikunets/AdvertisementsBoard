using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Domain.Users;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await TryGetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<UserDto>(user);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listUsers = await _repository.GetAll()
            .AsNoTracking()
            .ProjectTo<UserShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listUsers;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        var updatedDto = _mapper.Map<UserUpdatedDto>(entity);
        return updatedDto;
    }

    public async Task<bool> DoesUserExistWhereAsync(Expression<Func<User, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exist = await _repository.FindAnyAsync(filter, cancellationToken);
        return exist;
    }

    public async Task<UserDto> FindWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken)
    {
        var user = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
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