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
        var dto = await _repository.GetAll()
            .Where(u => u.Id == id)
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<UserShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .ProjectTo<UserShortInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(User entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> UpdateAsync(User updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        var dto = _mapper.Map<UserInfoDto>(updatedEntity);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(u => u.Id == id, cancellationToken);
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfExistsByEmailAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll().AnyAsync(u => u.Email == userEmail, cancellationToken);
    }
}