using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
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
    ///     Инициализирует экземпляр <see cref="UserRepository" />
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
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<UserDto>(entity);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .AsNoTracking()
            .ProjectTo<UserShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateAsync(UserDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<User>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        var updatedDto = _mapper.Map<UserUpdatedDto>(entity);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> DoesUserExistWhereAsync(Expression<Func<User, bool>> filter,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.FindAnyAsync(filter, cancellationToken);
        return exists;
    }

    public async Task<UserDto> FindWhereAsync(Expression<Func<User, bool>> filter, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAllFiltered(filter)
            .AsNoTracking()
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);
        return dto;
    }
}