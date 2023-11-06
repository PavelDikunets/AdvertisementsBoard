using AdvertisementsBoard.Application.AppServices.Contexts.Users.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Repositories;
using AdvertisementsBoard.Application.AppServices.Passwords.ErrorExceptions;
using AdvertisementsBoard.Application.AppServices.Passwords.Services;
using AdvertisementsBoard.Contracts.Users;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;

/// <inheritdoc />
public class UserService : IUserService
{
    private readonly ILogger<UserService> _logger;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly IUserRepository _userRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserService" />
    /// </summary>
    /// <param name="userRepository">Репозиторий для работы с пользователями.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="passwordService">Сервис для работы с паролями.</param>
    /// <param name="logger"></param>
    public UserService(IUserRepository userRepository, IMapper mapper, IPasswordService passwordService,
        ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _passwordService = passwordService;
        _mapper = mapper;
        _logger = logger;
    }

    /// <inheritdoc />
    public async Task<UserInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение пользователя по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        var infoDto = _mapper.Map<UserInfoDto>(dto);

        _logger.LogInformation("Пользователь успешно получен по Id: '{Id}'.", id);

        return infoDto;
    }

    /// <inheritdoc />
    public async Task<List<UserShortInfoDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Получение коллекции пользователей.");

        var dtos = await _userRepository.GetAllAsync(cancellationToken);

        _logger.LogInformation("Коллекция категорий успешно получена.");

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Создание пользователя '{Name}'.", dto.Name);

        if (!_passwordService.ComparePasswords(dto.Password, dto.ConfirmPassword))
        {
            _logger.LogInformation("Пароли не совпадают.");
            throw new PasswordMismatchException();
        }

        await CheckUserExistsByEmailAsync(dto.Email, cancellationToken);

        var userDto = _mapper.Map<UserDto>(dto);

        userDto.PasswordHash = _passwordService.HashPassword(dto.Password);

        var id = await _userRepository.CreateAsync(userDto, cancellationToken);

        _logger.LogInformation("Пользователь '{Name}' Id: '{Id}' успешно создан.", userDto.Name, id);

        return id;
    }

    /// <inheritdoc />
    public async Task<UserUpdatedDto> UpdateByIdAsync(Guid id, UserUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Обновление пользователя по Id: '{Id}'.", id);

        var currentDto = await _userRepository.FindWhereAsync(u => u.Id == id, cancellationToken);
        if (currentDto == null)
        {
            _logger.LogInformation("Пользователь не найден по Id: '{Id}'.", id);
            throw new UserNotFoundByIdException(id);
        }

        if (!_passwordService.VerifyPassword(currentDto.PasswordHash, updateDto.CurrentPassword))
        {
            _logger.LogInformation("Текущий пароль неверный.");
            throw new IncorrectCurrentPasswordException();
        }

        if (!_passwordService.ComparePasswords(updateDto.NewPassword, updateDto.ConfirmPassword))
        {
            _logger.LogInformation("Пароли не совпадают.");
            throw new PasswordMismatchException();
        }

        var currentUserName = currentDto.Name;

        _mapper.Map(updateDto, currentDto);

        currentDto.PasswordHash = _passwordService.HashPassword(updateDto.NewPassword);

        var updatedDto = await _userRepository.UpdateAsync(currentDto, cancellationToken);

        _logger.LogInformation("Пользователь '{Name}' успешно обновлен на '{updatedName}' Id: '{Id}'.",
            currentUserName, updatedDto.Name, id);

        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Удаление пользователя по Id: '{Id}'.", id);

        var dto = await TryGetByIdAsync(id, cancellationToken);

        await _userRepository.DeleteByIdAsync(dto.Id, cancellationToken);

        _logger.LogWarning("Пользователь удален по Id: '{Id}'.", id);
    }

    /// <inheritdoc />
    public async Task EnsureUserExistsByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос существования пользователя по Id: '{Id}'.", userId);

        var exist = await _userRepository.DoesUserExistWhereAsync(u => u.Id == userId, cancellationToken);

        if (!exist)
        {
            _logger.LogInformation("Пользователь не найден по Id: '{Id}'.", userId);
            throw new UserNotFoundByIdException(userId);
        }
    }


    private async Task<UserDto> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _userRepository.GetByIdAsync(id, cancellationToken);

        if (dto != null) return dto;

        _logger.LogInformation("Пользователь не найден по Id: '{Id}'", id);
        throw new UserNotFoundByIdException(id);
    }


    private async Task CheckUserExistsByEmailAsync(string userEmail, CancellationToken cancellationToken)
    {
        var exist = await _userRepository.DoesUserExistWhereAsync(u => u.Email == userEmail, cancellationToken);
        if (exist)
        {
            _logger.LogInformation("Такой адрес электронной почты уже используется");
            throw new UserAlreadyExistsByEmailException();
        }
    }
}