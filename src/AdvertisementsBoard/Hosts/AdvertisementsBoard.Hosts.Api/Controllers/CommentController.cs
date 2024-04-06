using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с вложениями.
/// </summary>
/// <response code="500">Произошла ошибка на стороне сервера.</response>
[ApiController]
[Route("comments")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly ILogger<CommentController> _logger;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CommentController" />
    /// </summary>
    /// <param name="commentService">Сервис для работы с комментариями.</param>
    /// <param name="logger">Логгер.</param>
    public CommentController(ICommentService commentService, ILogger<CommentController> logger)
    {
        _commentService = commentService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список комментариев к объявлению по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарии для объявления успешно получены.</response>
    /// <returns>Список комментариев.</returns>
    [HttpGet("/advertisemets/{id:guid}/comments")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<CommentInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCommentsByAdvertisementIdAsync([Required] Guid id,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка комментариев к объявлению Id: '{AdvertisementId}'.", id);

        var listOfComments = await _commentService.GetAllByAdvertisementIdAsync(id, cancellationToken);

        _logger.LogInformation("Список комментариев успешно получен к объявлению Id: '{AdvertisementId}'.", id);

        return Ok(listOfComments);
    }

    /// <summary>
    ///     Создать комментарий к объявлению по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="createDto">Модель создания комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="id">Идентификатор объявления.</param>
    /// <response code="201">Комментарий успешно создан.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Идентификатор созданного комментария.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost("/advertisements/{id:guid}/comments")]
    [Authorize]
    public async Task<IActionResult> UploadAsync([Required] Guid id, [FromBody] CommentCreateDto createDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос создания комментария: '{Comment}' к объявлению Id: '{AdvertisementId}'.",
            JsonConvert.SerializeObject(createDto), id);

        var userId = GetUserIdFromClaims();

        var createdCommentId = await _commentService.CreateAsync(id, userId, createDto, cancellationToken);

        var relativeUrl = Url.Action("GetById", new { id = createdCommentId });

        if (relativeUrl == null) throw new UrlGenerationException();

        _logger.LogInformation(
            "Комментарий успешно создан Id: '{CommentId}' '{Comment}' к объявлению Id: '{AdvertisementId}'.",
            createdCommentId, JsonConvert.SerializeObject(createDto), id);

        return Created(relativeUrl, createdCommentId);
    }

    /// <summary>
    ///     Получить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарий найден.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Модель комментария.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CommentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос комментария по Id: '{CommentId}'.", id);

        var comment = await _commentService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Комментарий успешно получен по Id: '{CommentId}' '{Comment}'.",
            id, JsonConvert.SerializeObject(comment));

        return Ok(comment);
    }

    /// <summary>
    ///     Обновить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="updateDto">Модель обновления комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарий успешно обновлено.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Модель c обновленной информацией о комментарии.</returns>
    [ProducesResponseType(typeof(CommentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync([Required] Guid id, [FromBody] CommentUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос обновления комментария по Id: '{CommentId} '{Comment}'.",
            id, JsonConvert.SerializeObject(updateDto));

        var userId = GetUserIdFromClaims();

        var updatedComment = await _commentService.UpdateByIdAsync(id, userId, updateDto, cancellationToken);

        _logger.LogInformation(
            "Комментарий успешно обновлен по Id: '{CommentId}', '{Comment}' пользователем Id: '{UserId}'.",
            id, JsonConvert.SerializeObject(updatedComment), userId);

        return Ok(updatedComment);
    }

    /// <summary>
    ///     Удалить комментарий по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Комментарий успешно удален.</response>
    /// <response code="404">Комментарий не найден.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос удаления комментария по Id: '{CommentId}'.", id);

        var userId = GetUserIdFromClaims();

        await _commentService.DeleteByIdAsync(id, userId, cancellationToken);

        _logger.LogInformation("Комментарий успешно удален по Id: '{CommentId}', пользователем Id: '{UserId}'.",
            id, userId);

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