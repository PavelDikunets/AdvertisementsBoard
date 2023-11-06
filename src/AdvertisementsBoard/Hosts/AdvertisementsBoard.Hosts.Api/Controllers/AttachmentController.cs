using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с вложениями.
/// </summary>
/// <response code="500">Произошла ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AttachmentController : ControllerBase
{
    private readonly IAttachmentService _attachmentService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentController" />
    /// </summary>
    /// <param name="attachmentService">Сервис для работы с вложениями.</param>
    public AttachmentController(IAttachmentService attachmentService)
    {
        _attachmentService = attachmentService;
    }

    /// <summary>
    ///     Получить вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложение найдено.</response>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Модель вложения <see cref="AttachmentInfoDto" />.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все вложения к объявлению.
    /// </summary>
    /// <param name="advertisementId">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложения для объявления успешно получены.</response>
    /// <response code="404">Объявление не найдено.</response>
    /// <returns>Список вложений <see cref="AttachmentInfoDto" />.</returns>
    [HttpGet("Get-all")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(List<AttachmentInfoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttachmentsByAdvertisementId([Required] Guid advertisementId,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Загрузить вложения к объявлению.
    /// </summary>
    /// <param name="dto">Модель загрузки вложения <see cref="AttachmentUploadDto" />.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Вложение успешно загружено.</response>
    /// <response code="404">Объявление не найдено.</response>
    /// <returns>Идентификатор загруженного вложения <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> UploadAsync([FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UploadByAdvertisementIdAsync(dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }

    /// <summary>
    ///     Обновить вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложение успешно обновлено.</response>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Модель c обновленной информацией о вложении <see cref="AttachmentUpdatedDto" />.</returns>
    [ProducesResponseType(typeof(AttachmentUpdatedDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, [FromForm] AttachmentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _attachmentService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}