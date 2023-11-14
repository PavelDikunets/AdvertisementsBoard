using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Common.Enums.Users;
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
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<UserDto>(user);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listUsers = await _userRepository.GetAllAsync(cancellationToken);

        var userDtos = _mapper.Map<List<UserShortInfoDto>>(listUsers);
        return userDtos;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserEditDto dto, CancellationToken cancellationToken)
    {
        var currentuser = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);

        _mapper.Map(dto, currentuser);

        var updatedUser = await _userRepository.UpdateAsync(currentuser, cancellationToken);

        var userDto = _mapper.Map<UserUpdatedDto>(updatedUser);
        return userDto;
    }

    /// <inheritdoc />
    public async Task<UserRoleDto> SetRoleByIdAsync(Guid id, UserRoleDto roleDto,
        CancellationToken cancellationToken)
    {
        if (!Enum.IsDefined(typeof(UserRole), roleDto.Role)) throw new InvalidRoleValueException();

        var user = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);

        _mapper.Map(roleDto, user);

        var dto = await _userRepository.UpdateAsync(user, cancellationToken);
        var updatedDto = _mapper.Map<UserRoleDto>(dto);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DoesUserExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.DoesUserExistWhereAsync(u => u.Id == id, cancellationToken);

        if (!exist) throw new UserNotFoundException(id);
    }

    /// <inheritdoc />
    public Task<bool> ValidateUserAsync(Guid currentUserid, Guid otherSourceUserId,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(currentUserid == otherSourceUserId);
    }
}