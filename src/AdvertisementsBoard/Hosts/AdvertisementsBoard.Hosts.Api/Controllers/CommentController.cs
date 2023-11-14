using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    /// <param name="logger"></param>
    public CommentController(ICommentService commentService, ILogger<CommentController> logger)
    {
        _commentService = commentService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить все комментарии к объявлению.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарии для объявления успешно получены.</response>
    /// <returns>Список комментариев.</returns>
    [HttpGet("/advertisemets/{id:guid}/comments")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<CommentInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllCommentsByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var listComments = await _commentService.GetAllByAdvertisementIdAsync(id, cancellationToken);
        return Ok(listComments);
    }

    /// <summary>
    ///     Создать комментарий к объявлению.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="dto">Модель создания комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="id"></param>
    /// <response code="201">Комментарий успешно создан.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Идентификатор созданного комментария.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost("/advertisements/{id:guid}/comments")]
    [Authorize]
    public async Task<IActionResult> UploadAsync(Guid id, [FromBody] CommentCreateDto dto,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        var result = await _commentService.CreateAsync(id, userId, dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }

    /// <summary>
    ///     Получить комментарий.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарий найден.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Модель комментария.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(CommentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _commentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Редактировать комментарий.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="dto">Модель редактирования комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Комментарий успешно обновлено.</response>
    /// <response code="404">Комментарий не найден.</response>
    /// <returns>Модель c обновленной информацией о комментарии.</returns>
    [ProducesResponseType(typeof(CommentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromBody] CommentEditDto dto,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        var updatedComment = await _commentService.UpdateByIdAsync(id, userId, dto, cancellationToken);
        return Ok(updatedComment);
    }

    /// <summary>
    ///     Удалить комментарий.
    /// </summary>
    /// <param name="id">Идентификатор комментария.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Комментарий успешно удален.</response>
    /// <response code="404">Комментарий не найден.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        await _commentService.DeleteByIdAsync(id, userId, cancellationToken);
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