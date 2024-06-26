using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с пользователями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("users")]
[Authorize(Policy = "NotBlocked")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserController" />
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    /// <param name="logger">Логирование.</param>
    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список пользователей.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Пользователи найдены.</response>
    /// <returns>Список моделей пользователей.</returns>
    [ProducesResponseType(typeof(List<UserShortInfoDto>), StatusCodes.Status200OK)]
    [HttpGet]
    [Authorize]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка пользователей.");

        var listOfUsers = await _userService.GetAllAsync(cancellationToken);

        _logger.LogInformation("Список пользователей успешно получен.");

        return Ok(listOfUsers);
    }

    /// <summary>
    ///     Получить текущего пользователя.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpGet("current")]
    [Authorize]
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUserInfoAsync(CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        _logger.LogInformation("Запрос информации о текущем пользователе по Id: '{UserId}'", userId);

        var user = await _userService.GetByIdAsync(userId, cancellationToken);

        _logger.LogInformation("Информация о пользователе '{NickName}' успешно получена Id: '{Id}'.",
            user.NickName, userId);

        return Ok(user);
    }

    /// <summary>
    ///     Получить пользователя по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя.</returns>
    /// <response code="200">Пользователь найден.</response>
    /// <response code="404">Пользователь не найден.</response>
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос пользователя по Id: '{Id}'", id);

        var user = await _userService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Пользователь '{NickName}' успешно получен по Id: '{Id}'.", user.NickName, id);

        return Ok(user);
    }


    /// <summary>
    ///     Обновить текущего пользователя.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="dto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Пользователь не найден.</response>
    /// <response code="200">Пользователь успешно обновлен.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель c обновленной информацией о пользователе.</returns>
    [ProducesResponseType(typeof(UserUpdatedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut("current")]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync([FromBody] UserUpdateDto dto, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        _logger.LogInformation("Запрос обновления пользователя Id: '{UserId}'.", userId);

        var updatedUser = await _userService.UpdateByIdAsync(userId, dto, cancellationToken);

        _logger.LogInformation("Пользователь '{NickName}' успешно обновлен на '{updatedName}' Id: '{UserId}'.",
            dto.Name, updatedUser.Name, userId);

        return Ok(updatedUser);
    }

    /// <summary>
    ///     Назначить роль пользователю по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: администратор.</permission>
    ///     <para>
    ///         Доступные значения ролей:
    ///         0 - Пользователь
    ///         1 - Модератор
    ///         2 - Администратор
    ///     </para>
    /// </remarks>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="dto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Пользователь не найден.</response>
    /// <response code="200">Пользователь успешно обновлен.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель c обновленной информацией о пользователе.</returns>
    [ProducesResponseType(typeof(UserRoleDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPatch("{id:guid}/role")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> SetRoleByIdAsync(Guid id, [FromBody] UserRoleDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос установки роли для пользователя по Id: '{Id}'.", id);

        var userRoleDto = await _userService.SetRoleByIdAsync(id, dto, cancellationToken);

        _logger.LogInformation("Роль '{RoleName}' успешно установлена пользователю по Id: '{Id}'.",
            userRoleDto.Role, id);

        return Ok(userRoleDto);
    }


    private Guid GetUserIdFromClaims()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId)) throw new AuthenticationFailedException();
        return userId;
    }
}