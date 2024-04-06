using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Common.Enums.Users;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Users;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserService" />
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    /// <param name="mapper">Маппер.</param>
    public UserService(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<UserDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.GetByIdAsync(id, cancellationToken);

        var userDto = _mapper.Map<UserDto>(userEntity);
        return userDto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var userEntities = await _userRepository.GetAllAsync(cancellationToken);

        var userDtos = _mapper.Map<List<UserShortInfoDto>>(userEntities);
        return userDtos;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var userEntity = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);

        _mapper.Map(updateDto, userEntity);

        var updatedUserEntity = await _userRepository.UpdateAsync(userEntity, cancellationToken);

        var updatedUserDto = _mapper.Map<UserUpdatedDto>(updatedUserEntity);
        return updatedUserDto;
    }

    /// <inheritdoc />
    public async Task<UserRoleDto> SetRoleByIdAsync(Guid id, UserRoleDto roleDto,
        CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(UserRole), roleDto.Role)) throw new InvalidRoleValueException();

        var userEntity = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);

        _mapper.Map(roleDto, userEntity);

        var updatedUserEntity = await _userRepository.UpdateAsync(userEntity, cancellationToken);

        var updatedUserDto = _mapper.Map<UserRoleDto>(updatedUserEntity);
        return updatedUserDto;
    }

    /// <inheritdoc />
    public async Task DoesUserExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userExists = await _userRepository.DoesUserExistWhereAsync(u => u.Id == id, cancellationToken);

        if (!userExists) throw new UserNotFoundException(id);
    }

    /// <inheritdoc />
    public Task CheckUserPermissionAsync(Guid currentUserid, Guid otherSourceUserId,
        CancellationToken cancellationToken)
    {
        if (currentUserid != otherSourceUserId) throw new PermissionException();
        return Task.CompletedTask;
    }
}