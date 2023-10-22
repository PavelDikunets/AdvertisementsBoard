using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.PasswordHasher;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Domain.Users;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly IPasswordHasherService _passwordHasherService;
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserService" />
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="passwordHasherService">Хешер паролей.</param>
    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordHasherService passwordHasherService)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasherService = passwordHasherService;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (dto == null) throw new NotFoundException($"Пользователь с идентификатором {id} не найден.");

        var infoDto = _mapper.Map<UserInfoDto>(dto);
        return infoDto;
    }

    /// <inheritdoc />
    public async Task<UserShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var dtos = await _userRepository.GetAllAsync(cancellationToken);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken)
    {
        await ComparePasswordsAsync(dto.Password, dto.ConfirmPassword, cancellationToken);

        await CheckIfExistsByEmailAsync(dto.Email, cancellationToken);
        var userDto = _mapper.Map<UserDto>(dto);

        userDto.PasswordHash = _passwordHasherService.HashPassword(dto.Password);

        var entity = _mapper.Map<User>(userDto);

        var id = await _userRepository.CreateAsync(entity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> UpdateByIdAsync(Guid id, UserUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);

        var currentDto = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (!_passwordHasherService.VerifyPassword(currentDto.PasswordHash, updateDto.CurrentPassword))
            throw new PasswordException("Текущий пароль неверный.");

        await ComparePasswordsAsync(updateDto.NewPassword, updateDto.ConfirmPassword, cancellationToken);

        _mapper.Map(updateDto, currentDto);

        currentDto.PasswordHash = _passwordHasherService.HashPassword(updateDto.NewPassword);

        var entity = _mapper.Map<User>(currentDto);

        var updatedDto = await _userRepository.UpdateAsync(entity, cancellationToken);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await TryFindByIdAsync(id, cancellationToken);
        await _userRepository.DeleteByIdAsync(id, cancellationToken);
    }


    public async Task TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.TryFindByIdAsync(id, cancellationToken);
        if (!exists) throw new NotFoundException($"Пользователь с идентификатором {id} не найден.");
    }

    private async Task CheckIfExistsByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var exists = await _userRepository.CheckIfExistsByEmailAsync(email, cancellationToken);
        if (exists)
            throw new AlreadyExistsException($"Пользователь с адресом электронной почты {email} уже существует.");
    }

    private static Task ComparePasswordsAsync(string password, string confirmPassword,
        CancellationToken cancellationToken)
    {
        if (password != confirmPassword) throw new PasswordException("Пароли не совпадают.");
        return Task.CompletedTask;
    }
}