using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с вложениями.
/// </summary>
/// <response code="500">Произошла ошибка на стороне сервера.</response>
[ApiController]
[Route("advertisements/")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;
    private readonly ILogger<AttachmentController> _logger;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentController" />
    /// </summary>
    /// <param name="attachmentService">Сервис для работы с вложениями.</param>
    /// <param name="logger"></param>
    public AttachmentController(IAttachmentService attachmentService, ILogger<AttachmentController> logger)
    {
        _attachmentService = attachmentService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список вложений к объявлению по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложения для объявления успешно получены.</response>
    /// <response code="404">Объявление не найдено.</response>
    /// <returns>Список вложений.</returns>
    [HttpGet("{id:guid}/attachments")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<AttachmentInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttachmentsByAdvertisementId(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetAllByAdvertisementIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Загрузить вложения к объявлению по идентификатору.
    /// </summary>
    /// <remarks>
    ///     <permission>Уровень доступа: авторизованный пользователь.</permission>
    /// </remarks>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель загрузки вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Вложение успешно загружено.</response>
    /// <response code="404">Объявление не найдено.</response>
    /// <returns>Идентификатор загруженного вложения.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost("{id:guid}/attachments")]
    [Authorize]
    public async Task<IActionResult> UploadAsync(Guid id, [FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        var result = await _attachmentService.UploadByAdvertisementIdAsync(id, userId, dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }

    /// <summary>
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложение найдено.</response>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Модель вложения.</returns>
    [HttpGet("/attachments/{id:guid}")]
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Обновить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="dto">Модель редактирования вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложение успешно обновлено.</response>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Модель c обновленной информацией о вложении.</returns>
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPut("/attachments/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromForm] AttachmentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        var updatedAttachment = await _attachmentService.UpdateByIdAsync(id, userId, dto, cancellationToken);
        return Ok(updatedAttachment);
    }

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("/attachments/{id:guid}")]
    [Authorize]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userId = GetUserIdFromClaims();

        await _attachmentService.DeleteByIdAsync(id, userId, cancellationToken);
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