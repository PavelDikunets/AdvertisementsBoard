using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с подкатегорями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _subCategoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryController" />
    /// </summary>
    /// <param name="subCategoryService">Сервис для работы с подкатегориями.</param>
    public SubCategoryController(ISubCategoryService subCategoryService)
    {
        _subCategoryService = subCategoryService;
    }

    /// <summary>
    ///     Получить подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории <see cref="SubCategoryInfoDto" />.</returns>
    /// <response code="200">Подкатегории найдена.</response>
    /// <response code="404">Подкатегории не найдена.</response>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(SubCategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Подкатегории найдены.</response>
    /// <returns>Массив подкатегорий с краткой информацией <see cref="SubCategoryShortInfoDto" />.</returns>
    [ProducesResponseType(typeof(SubCategoryShortInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать подкатегорию по идентификатору категории.
    /// </summary>
    /// <param name="dto">Модель создания подкатегории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Подкатегория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданной подкатегории <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Обновить подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="dto">Модель обновления подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Подкатегории не найдена.</response>
    /// <response code="200">Подкатегории успешно обновлена.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель обновления подкатегории <see cref="SubCategoryUpdateDto" />.</returns>
    [ProducesResponseType(typeof(SubCategoryUpdateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, SubCategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Подкатегория успешно удалена.</response>
    /// <response code="404">Подкатегория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subCategoryService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}