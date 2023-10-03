using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с объявлениями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;
    private readonly IAttachmentService _attachmentService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementController" />
    /// </summary>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    /// <param name="attachmentService">Серив для работы с вложениями.</param>
    public AdvertisementController(IAdvertisementService advertisementService, IAttachmentService attachmentService)
    {
        _advertisementService = advertisementService;
        _attachmentService = attachmentService;
    }

    /// <summary>
    ///     Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" />.</returns>
    /// <response code="200">Объявление найдено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpGet]
    [ProducesResponseType(typeof(AdvertisementInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <response code="200">Объявления найдены.</response>
    /// <returns>Массив объявлений <see cref="AdvertisementShortInfoDto" /></returns>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all-paged")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageNumber = 0,
        int pageSize = 10)
    {
        var result = await _advertisementService.GetAllAsync(cancellationToken, pageSize, pageNumber);
        return Ok(result);
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    /// <param name="dto">Модель создания объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Объявление успешно создано.</response>
    /// <response code="404">Категория не найдена.</response>
    /// <returns>Идентификатор созданного объявления <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync([Required] Guid categoryId, AdvertisementCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _advertisementService.CreateAsync(categoryId, dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Редактировать объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Объявление не найдено.</response>
    /// <response code="200">Объявление успешно обновлено.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель объявления <see cref="AdvertisementUpdateDto" />.</returns>
    [ProducesResponseType(typeof(AdvertisementUpdateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([Required] Guid id,
        AdvertisementUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _advertisementService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _advertisementService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Получить все вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Массив вложений <see cref="AttachmentInfoDto" />></returns>
    /// <response code="200">Вложения для объявления успешно получены.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpGet("{id:guid}/Attachments")]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(AttachmentInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAttachmentsByAdvertisementId([Required] Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.GetAllByAdvertisementIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Загрузить вложения по идентификатору объявления.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="dto">Модель вложения.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Вложение успешно загружено.</response>
    /// <response code="404">Объявление не найдено.</response>
    /// <returns>Идентификатор загруженного вложения <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost("{id:guid}/Attachments")]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> UploadAsync([Required] Guid id, [FromForm] AttachmentUploadDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _attachmentService.UploadByAdvertisementIdAsync(id, dto, cancellationToken);
        return Created(nameof(UploadAsync), result);
    }
}