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
    /// <returns>Модель объявления <see cref="AdvertisementInfoDto" />.</returns>
    /// <response code="200">Объявление найдено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpGet("Get-by-id/")]
    [ProducesResponseType(typeof(AdvertisementInfoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _advertisementService.GetByIdAsync(id, cancellationToken);
            return Ok(result);
        }
        catch (ArgumentNullException)
        {
            throw new ArgumentNullException($"Объявление по идентификатору {id} не найдено!");
        }
    }

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция объявлений <see cref="AdvertisementInfoDto" /></returns>
    /// <response code="200">Объявления найдены.</response>
    /// <response code="404">Объявления не найдены.</response>
    [ProducesResponseType(typeof(AdvertisementShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all-paged")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        var result = await _advertisementService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель создания объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Идентификатор объявления.</returns>
    /// <response code="201">Объявление успешно создано.</response>
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AdvertisementCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
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
            return NotFound(dto.Id);
        }
    }

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Объявление успешно удалено.</response>
    /// <response code="404">Объявление не найдено.</response>
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            await _advertisementService.DeleteByIdAsync(id, cancellationToken);
            return Ok();
        }
        catch (ArgumentNullException)
        {
            return NotFound();
        }
    }
}