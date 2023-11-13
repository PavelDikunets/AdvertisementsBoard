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
    public async Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);

        var userDto = _mapper.Map<UserInfoDto>(user);
        return userDto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var listUsers = await _userRepository.GetAllAsync(cancellationToken);
        return listUsers;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserEditDto dto, CancellationToken cancellationToken)
    {
        var user = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);

        var updatedUser = _mapper.Map(dto, user);

        var updatedDto = await _userRepository.UpdateAsync(updatedUser, cancellationToken);

        return updatedDto;
    }

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

    public async Task DoesUserExistByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.DoesUserExistWhereAsync(u => u.Id == id, cancellationToken);

        if (!exist) throw new UserNotFoundException(id);
    }

    public Task<bool> ValidateUserAsync(Guid userId, Guid userIdFromAdvertisement,
        CancellationToken cancellationToken)
    {
        return Task.FromResult(userId == userIdFromAdvertisement);
    }
}