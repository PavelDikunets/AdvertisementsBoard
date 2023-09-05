using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;
using AdvertisementsBoard.Contracts.Advertisements;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с объявлениями.
/// </summary>
[ApiController]
[Route("[controller]")]
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
    /// <returns>Модель объявления <see cref="AdvertisementDto" /></returns>
    /// <response code="200">Ресурс успешно найден.</response>
    /// <response code="404">Ресурс не найден.</response>
    /// <response code="500">Произошла ошибка на стороне сервера.</response>
    [HttpGet("Get-by-id")]
    [ProducesResponseType(typeof(AdvertisementDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _advertisementService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить постраничные объявления.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <param name="pageSize">Размер страницы.</param>
    /// <param name="pageIndex">Номер страницы.</param>
    /// <returns>Коллекция объявлений <see cref="AdvertisementDto" /></returns>
    [HttpGet("Get-all-paged")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken, int pageSize = 10,
        int pageIndex = 0)
    {
        return Ok();
    }

    /// <summary>
    ///     Создать объявление.
    /// </summary>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpPost]
    public async Task<IActionResult> CreateAsync(AdvertisementDto dto, CancellationToken cancellationToken)
    {
        return Created(string.Empty, null);
    }

    /// <summary>
    ///     Редактировать объявление.
    /// </summary>
    /// <param name="dto">Модель объявления</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpPut]
    public async Task<IActionResult> UpdateAsync(AdvertisementDto dto, CancellationToken cancellationToken)
    {
        return Ok();
    }

    /// <summary>
    ///     Удалить объявление по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор объявления.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return Ok();
    }
}