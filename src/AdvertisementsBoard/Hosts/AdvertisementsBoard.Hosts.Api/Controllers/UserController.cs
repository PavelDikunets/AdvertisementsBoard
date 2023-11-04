using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.Users;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с пользователями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="UserController" />
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    /// <summary>
    ///     Получить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель пользователя <see cref="UserInfoDto" />.</returns>
    /// <response code="200">Пользователь найден.</response>
    /// <response code="404">Пользователь не найден.</response>
    [ProducesResponseType(typeof(UserInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _userService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить всех пользователей.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Пользователи найдены.</response>
    /// <returns>Список моделей пользователей <see cref="UserShortInfoDto" />.</returns>
    [ProducesResponseType(typeof(List<UserShortInfoDto>), StatusCodes.Status200OK)]
    [HttpGet("Get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _userService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать пользователя.
    /// </summary>
    /// <param name="dto">Модель создания пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Пользователь успешно создан.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданного пользователя <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(UserCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _userService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Обновить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="dto">Модель обновления пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Пользователь не найден.</response>
    /// <response code="200">Пользователь успешно обновлен.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель c обновленной информацией о пользователе <see cref="UserUpdatedDto" />.</returns>
    [ProducesResponseType(typeof(UserUpdatedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, UserUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _userService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить пользователя.
    /// </summary>
    /// <param name="id">Идентификатор пользователя.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Пользователь успешно удален.</response>
    /// <response code="404">Пользователь не найден.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _userService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}