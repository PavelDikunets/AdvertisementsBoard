using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с объявлениями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public class AdvertisementController : ControllerBase
{
    private readonly IAdvertisementService _advertisementService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementController" />
    /// </summary>
    /// <param name="advertisementService">Сервис для работы с объявлениями.</param>
    public AdvertisementController(IAdvertisementService advertisementService)
    {
        _advertisementService = advertisementService;
    }

    /// <summary>
    ///     Получить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель объявления <see cref="AdvertisementDto" />.</returns>
    /// <response code="200">Объявление найдено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpGet("Get-by-id")]
    [ProducesResponseType(typeof(AdvertisementInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (NullReferenceException ex)
        {
            return NotFound();
        }
    }

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageNumber">Номер страницы.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <response code="200">Объявления найдены.</response>
    /// <returns>Массив объявлений <see cref="AdvertisementDto" /></returns>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all-paged")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageNumber=0, int pageSize=10)
    {
        var result = await _advertisementService.GetAllAsync(cancellationToken, pageSize, pageNumber);
        return Ok(result);
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель создания объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор объявления.</returns>
    /// <response code="201">Объявление успешно создано.</response>
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Объявление не найдено.</response>
    /// <response code="200">Объявление успешно обновлено.</response>
    /// <response code="400">Некорректный запрос.</response>
    [ProducesResponseType(typeof(ExistingAdvertisementUpdateDto), StatusCodes.Status200OK)]
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(ExistingAdvertisementUpdateDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.UpdateAsync(dto, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _advertisementService.DeleteByIdAsync(id, cancellationToken);
            return NoContent();
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }
}