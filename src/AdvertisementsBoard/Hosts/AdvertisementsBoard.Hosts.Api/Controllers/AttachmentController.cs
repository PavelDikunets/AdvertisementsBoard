using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Contracts.Attachments;
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
    /// <returns>Модель объявления <see cref="AttachmentDto" /></returns>
    /// <response code="200">Ok.</response>
    /// <response code="404">Не найдено.</response>
    [HttpGet("Get-by-id")]
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить постраничные вложения.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция вложений <see cref="AttachmentDto" /></returns>
    /// <response code="200">Ok.</response>
    /// <response code="404">Не найдены.</response>
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("Get-all-paged")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        try
        {
            var result = await _attachmentService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }

    /// <summary>
    ///     Загрузить вложение.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Вложение успешно загружено.</response>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAsync(Guid id, [FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UploadByIdAsync(id, dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }

    /// <summary>
    ///     Редактировать вложение.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(Guid id, ExistingAttachmentUpdateDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _attachmentService.UpdateAsync(dto, cancellationToken);
            return Ok(result);
        }

        catch (ArgumentNullException)
        {
            return NotFound(dto.Id);
        }
    }

    /// <summary>
    ///     Удалить вложение по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _attachmentService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (ArgumentNullException)
        {
            return NotFound(id);
        }
    }
}