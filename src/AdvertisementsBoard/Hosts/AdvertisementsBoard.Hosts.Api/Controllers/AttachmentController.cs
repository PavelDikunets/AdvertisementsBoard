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
    ///     Получить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Вложение найдено.</response>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Модель вложения <see cref="AttachmentInfoDto" />.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Загрузить вложение.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Вложение успешно загружено.</response>
    /// <returns>Идентификатор загруженного вложения <see cref="Guid" />.</returns>
    [ProducesResponseType(StatusCodes.Status201Created)]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAsync([Required] Guid id, [FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UploadByIdAsync(id, dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Вложение не найдено.</response>
    /// <returns>Идентификатор вложения <see cref="Guid" />.</returns>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync([Required] Guid id, [FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _attachmentService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}