using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;
using AdvertisementsBoard.Contracts.Accounts;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с пользователями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("accounts")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ILogger<AccountController> _logger;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AccountController" />
    /// </summary>
    /// <param name="logger">Логирование.</param>
    /// <param name="accountService">Сервис для работы с аккаунтами.</param>
    public AccountController(ILogger<AccountController> logger, IAccountService accountService)
    {
        _accountService = accountService;
        _logger = logger;
    }

    /// <summary>
    ///     Зарегистрировать аккаунт.
    /// </summary>
    /// <param name="createDto">Модель регистрации аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Аккаунт успешно создан.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="409">Конфликт.</response>
    /// <returns>Модель созданного аккаунта.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> SignUpAsync([FromBody] AccountCreateDto createDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос регистрации аккаунта пользователя '{User}'.",
            JsonConvert.SerializeObject(createDto.User));

        var createdAccountId = await _accountService.SignUpAsync(createDto, cancellationToken);

        _logger.LogInformation("Аккаунт Id: '{AccountId}' пользователя '{User}' успешно зарегистрирован.",
            createdAccountId, JsonConvert.SerializeObject(createDto.User));

        var relativeUrl = Url.Action("GetById", new { id = createdAccountId });

        if (relativeUrl == null) throw new UrlGenerationException();

        return Created(relativeUrl, createdAccountId);
    }

    /// <summary>
    ///     Вход в аккаунт.
    /// </summary>
    /// <param name="dto">Модель аутентификации.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Успешный вход в аккаунт.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <response code="401">Ошибка авторизации.</response>
    /// <returns>Jwt токен.</returns>
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [HttpPost("signin")]
    [AllowAnonymous]
    public async Task<IActionResult> SignInAsync([FromBody] AccountSignInDto dto, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос входа в аккаунт.");

        var token = await _accountService.SignInAsync(dto, cancellationToken);

        _logger.LogInformation("Успешный вход в аккаунт.");

        return Ok(token);
    }

    /// <summary>
    ///     Получить список аккаунтов с постраничной навигацией.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: администратор.</permission>
    /// </remarks>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="isBlocked">Фильтрация по статусу блокировки.</param>
    /// <response code="200">Список аккаунтов успешно получен.</response>
    /// <response code="401">Ошибка авторизации.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <returns>Список аккаунтов.</returns>
    [ProducesResponseType(typeof(List<AccountShortInfoDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [HttpGet]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken,
        [Range(0, int.MaxValue)] int pageNumber = 0,
        [Range(1, 100)] int pageSize = 10,
        bool isBlocked = false)
    {
        _logger.LogInformation("Запрос списка аккаунтов.");

        var listOfAccounts = await _accountService.GetAllAsync(pageSize, pageNumber, isBlocked, cancellationToken);

        _logger.LogInformation("Список аккаунтов успешно получен.");

        return Ok(listOfAccounts);
    }

    /// <summary>
    ///     Получить аккаунт по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission> Уровень доступа: администратор.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Аккаунт успешно получен.</response>
    /// <response code="401">Ошибка авторизации.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <response code="404">Аккаунт не найден.</response>
    /// <returns>Модель с информацией об аккаунте.</returns>
    [ProducesResponseType(typeof(AccountAdminDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос аккаунта по Id: {Id}.", id);

        var account = await _accountService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Аккаунт успешно получен: '{Account}' .", JsonConvert.SerializeObject(account));

        return Ok(account);
    }

    /// <summary>
    ///     Получить текущий аккаунт.
    /// </summary>
    /// <param name="cancellationToken"></param>
    /// <response code="200">Аккаунт успешно получен.</response>
    /// <response code="401">Ошибка авторизации.</response>
    /// <response code="404">Аккаунт не найден.</response>
    /// <returns>Модель с информацией об аккаунте.</returns>
    [ProducesResponseType(typeof(AccountInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("current")]
    [Authorize]
    public async Task<IActionResult> GetCurrentByUserIdAsync(CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        _logger.LogInformation("Запрос аккаунта пользователем Id: '{Id}'.", userId);

        var account = await _accountService.GetByUserIdAsync(userId, cancellationToken);

        _logger.LogInformation("Аккаунт успешно получен пользователем Id: '{Id}'.", userId);

        return Ok(account);
    }

    /// <summary>
    ///     Изменить пароль текущего аккаунта.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="dto">Модель смены пароля.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Пароль успешно изменен.</response>
    /// <response code="400">Некорректный запрос.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [HttpPatch("password")]
    [Authorize]
    public async Task<IActionResult> ChangePasswordAsync([FromBody] AccountPasswordEditDto dto,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        _logger.LogInformation("Запрос изменения пароля пользователем Id: '{UserId}'.", userId);

        await _accountService.ChangePasswordAsync(userId, dto, cancellationToken);

        _logger.LogInformation("Пароль пользователя Id: '{UserId}' успешно изменен.", userId);
        return Ok();
    }

    /// <summary>
    ///     Заблокировать аккаунт по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: администратор.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="statusDto">Модель блокировки аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Аккаунт не найден.</response>
    /// <response code="200">Аккаунт успешно заблокирован.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель c обновленной информацией о блокировке аккаунта.</returns>
    [ProducesResponseType(typeof(AccountBlockStatusDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [HttpPatch("{id:guid}/block")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> BlockByIdAsync([Required] Guid id, [FromBody] AccountBlockStatusDto statusDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос установки блоровки аккаунта по Id: '{Id}' в статус '{IsBlocked}'.",
            id, statusDto.IsBlocked);

        var account = await _accountService.BlockByIdAsync(id, statusDto, cancellationToken);

        _logger.LogInformation("Статус блокировки аккаунта по Id: '{Id}' успешно установлен в '{IsBlocked}'.",
            id, account.IsBlocked);

        return Ok(account);
    }

    /// <summary>
    ///     Удалить аккаунт по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: администратор.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор аккаунта.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Аккаунт не найден.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <response code="204">Аккаунт успешно удален.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос удаления аккаунта по Id: '{Id}'.", id);

        await _accountService.DeleteByIdAsync(id, cancellationToken);

        _logger.LogInformation("Аккаунт успешно удален по Id: '{Id}'.", id);

        return NoContent();
    }


    private Guid GetUserIdFromClaims()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId)) throw new AuthenticationFailedException();
        return userId;
    }
}