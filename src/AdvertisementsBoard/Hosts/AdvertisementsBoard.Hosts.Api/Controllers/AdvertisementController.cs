using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с объявлениями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("advertisements")]
[Authorize(Policy = "NotBlocked")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly ILogger<AdvertisementController> _logger;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementController" />
    /// </summary>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="logger"></param>
    public AdvertisementController(IAdvertisementService advertisementService, ILogger<AdvertisementController> logger)
    {
        _advertisementService = advertisementService;
        _logger = logger;
    }


    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <response code="200">Объявления найдены.</response>
    /// <returns>Массив объявлений с краткой информацией.</returns>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 0,
        int pageSize = 10)
    {
        _logger.LogInformation("Запрос списка объявлений.");

        var result = await _advertisementService.GetAllAsync(cancellationToken, pageSize, pageNumber);

        _logger.LogInformation("Список объявлений успешно получен.");

        return Ok(result);
    }

    /// <summary>
    ///     Получить список объявлений пользователя.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Объявления найдены.</response>
    /// <returns>Массив объявлений с краткой информацией.</returns>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("/users/current/advertisements")]
    [Authorize]
    public async Task<IActionResult> GetAllByUserIdAsync(CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        _logger.LogInformation("Запрос списка объявлений аутентифицированного пользователя Id: {userId}.", userId);

        var result = await _advertisementService.GetAllByUserIdAsync(userId, cancellationToken);

        _logger.LogInformation("Список объявлений пользователя успешно получен.");

        return Ok(result);
    }

    /// <summary>
    ///     Получить объявление.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель с информацией об объявлении.</returns>
    /// <response code="200">Объявление найдено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(AdvertisementInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос объявления по Id: {Id}.", id);

        var result = await _advertisementService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Объявление по '{Id}' успешно получено.", id);

        return Ok(result);
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="dto">Модель создания объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Объявление успешно создано.</response>
    /// <response code="404">Не найдено.</response>
    /// <returns>Идентификатор созданного объявления.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(AdvertisementCreateDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос создания объявления '{Title}'.", dto.Title);

        var userId = GetUserIdFromClaims();

        var id = await _advertisementService.CreateAsync(dto, userId, cancellationToken);

        _logger.LogInformation("Объявление '{Title}' успешно создано Id: {Id}.", dto.Title, id);

        return Created(nameof(CreateAsync), id);
    }

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Не найдено.</response>
    /// <response code="200">Объявление успешно обновлено.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <returns>Модель обновленного объявления.</returns>
    [ProducesResponseType(typeof(AdvertisementInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, AdvertisementEditDto dto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос изменения объявления: {dto}, Id: '{Id}'.", id, dto);

        var userId = GetUserIdFromClaims();

        var updatedDto = await _advertisementService.UpdateByIdAsync(id, userId, dto, cancellationToken);

        _logger.LogInformation("Объявление успешно изменено: {dto}, Id: '{Id}'.", id, updatedDto);

        return Ok(updatedDto);
    }

    /// <summary>
    ///     Удалить объявление.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос удаления объявления по Id: '{Id}'.", id);

        var userId = GetUserIdFromClaims();

        await _advertisementService.DeleteByIdAsync(id, userId, cancellationToken);

        _logger.LogInformation("Объявление успешно удалено по Id: '{Id}'.", id);

        return NoContent();
    }


    private Guid GetUserIdFromClaims()
    {
        var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdString, out var userId))
            if (userIdString != null)
                throw new InvalidUserIdException(userIdString);

        return userId;
    }
}