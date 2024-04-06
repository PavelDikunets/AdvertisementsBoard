using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    /// <param name="logger">Логирование.</param>
    public AdvertisementController(IAdvertisementService advertisementService, ILogger<AdvertisementController> logger)
    {
        _advertisementService = advertisementService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список объявлений с постраничной навигацией.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <response code="200">Объявления найдены.</response>
    /// <returns>Массив объявлений с краткой информацией.</returns>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken,
        [Range(0, 100)] int pageNumber = 0,
        [Range(1, int.MaxValue)] int pageSize = 10)
    {
        _logger.LogInformation("Запрос списка объявлений.");

        var listOfAdvertisements = await _advertisementService.GetAllAsync(cancellationToken, pageSize, pageNumber);

        _logger.LogInformation("Список объявлений успешно получен.");

        return Ok(listOfAdvertisements);
    }

    /// <summary>
    ///     Получить список объявлений текущего пользователя.
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

        _logger.LogInformation("Запрос списка объявлений пользователя Id: '{UserId}'.", userId);

        var listOfAdvertisements = await _advertisementService.GetAllByUserIdAsync(userId, cancellationToken);

        _logger.LogInformation("Список объявлений пользователя Id: '{UserId}' успешно получен.", userId);

        return Ok(listOfAdvertisements);
    }

    /// <summary>
    ///     Получить объявление по идентификатору.
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
        _logger.LogInformation("Запрос объявления по Id: {AdvertisementId}.", id);

        var advertisement = await _advertisementService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Объявление успешно получено: '{Advertisement}'.",
            JsonConvert.SerializeObject(advertisement));

        return Ok(advertisement);
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="createDto">Модель создания объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Объявление успешно создано.</response>
    /// <response code="404">Не найдено.</response>
    /// <returns>Идентификатор созданного объявления.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateAsync(AdvertisementCreateDto createDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос создания объявления '{Advertisement}'.", JsonConvert.SerializeObject(createDto));

        var userId = GetUserIdFromClaims();

        var createdAdvertisementId = await _advertisementService.CreateAsync(createDto, userId, cancellationToken);

        var relativeUrl = Url.Action("GetById", new { id = createdAdvertisementId });

        if (relativeUrl == null) throw new UrlGenerationException();

        _logger.LogInformation("Объявление  успешно создано: '{AdvertisementId}' пользователем Id: '{UserId}'.",
            JsonConvert.SerializeObject(createdAdvertisementId), userId);

        return Created(relativeUrl, createdAdvertisementId);
    }

    /// <summary>
    ///     Обновить объявление по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="updateDto">Модель объявления</param>
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
    public async Task<IActionResult> UpdateByIdAsync(Guid id, AdvertisementUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос обновления объявления по Id: '{AdvertisementId}'.", id);

        var userId = GetUserIdFromClaims();

        var updatedAdvertisement =
            await _advertisementService.UpdateByIdAsync(id, userId, updateDto, cancellationToken);

        _logger.LogInformation(
            "Объявление успешно обновлено по Id: '{AdvertisementId}' '{Advertisement}' пользователем Id: '{UserId}'.",
            id, JsonConvert.SerializeObject(updatedAdvertisement), userId);

        return Ok(updatedAdvertisement);
    }

    /// <summary>
    ///     Удалить объявление по идентификатору.
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
        _logger.LogInformation("Запрос удаления объявления по Id: '{AdvertisementId}'.", id);

        var userId = GetUserIdFromClaims();

        await _advertisementService.DeleteByIdAsync(id, userId, cancellationToken);

        _logger.LogInformation("Объявление успешно удалено по Id: '{AdvertisementId}' пользователем Id: '{UserId}'.",
            id, userId);

        return NoContent();
    }


    private Guid GetUserIdFromClaims()
    {
        var userIdValue = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (!Guid.TryParse(userIdValue, out var userId)) throw new AuthenticationFailedException();
        return userId;
    }
}