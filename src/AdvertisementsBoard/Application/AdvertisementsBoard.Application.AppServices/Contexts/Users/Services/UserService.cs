using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Domain.Users;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    /// <summary>
    /// Инициализирует экземпляр <see cref="UserService"/>
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);
        
        var dto = new UserInfoDto
        {
            Name = entity.Name,
            Email = entity.Email
        };
        return dto;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = await _userRepository.GetAllAsync(cancellationToken);

        var dtos = entities.Select(u => new UserInfoDto
        {
            Name = u.Name,
            Email = u.Email
        }).ToArray();

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken)
    {
        if (dto.Password != dto.ConfirmPassword) throw new PasswordMismatchException("Пароли не совпадают.");

        await CheckIfExistsByEmailAsync(dto.Email, cancellationToken);

        var entity = new User
        {
            Name = dto.Name,
            Email = dto.Email,
            Password = dto.Password
        };

        var id = await _userRepository.CreateAsync(entity, cancellationToken);

        return id;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> UpdateByIdAsync(Guid id, UserUpdateDto dto, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        await CheckIfExistsByEmailAsync(dto.Email, cancellationToken);

        entity.Name = dto.Name;

        await _userRepository.UpdateAsync(entity, cancellationToken);

        var updateDto = new UserInfoDto
        {
            Name = entity.Name
        };

        return updateDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private async Task<User> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Пользователь с идентификатором {id} не найден.");
        return entity;
    }

    private async Task CheckIfExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.CheckIfExistsByNameAndEmailAsync(email, cancellationToken);

        if (exists) throw new AlreadyExistsException($"Пользователь с адресом электронной почты {email} уже существует.");
    }
}