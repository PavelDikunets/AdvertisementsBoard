using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Domain.Users;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Users.Repositories;

/// <inheritdoc />
public class UserRepository : IUserRepository
{
    private readonly IBaseDbRepository<User> _repository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserRepository"/>
    /// </summary>
    /// <param name="repository">Репозиторий пользователей.</param>
    public UserRepository(IBaseDbRepository<User> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<User> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        return entity;
    }

    /// <inheritdoc />
    public async Task<IEnumerable<User>> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAll().ToArrayAsync(cancellationToken);
        return entities;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(User entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task UpdateAsync(User updatedEntity, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> CheckIfExistsByNameAndEmailAsync(string userEmail,
        CancellationToken cancellationToken)
    {
        return await _repository.GetAll().AnyAsync(u => u.Email == userEmail, cancellationToken);
    }
}