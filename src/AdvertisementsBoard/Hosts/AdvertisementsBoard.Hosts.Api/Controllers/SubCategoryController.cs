using System.ComponentModel.DataAnnotations;
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
    private readonly ISubCategoryService _subcategoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryController" />
    /// </summary>
    /// <param name="subcategoryService">Сервис для работы с подкатегориями.</param>
    public SubCategoryController(ISubCategoryService subcategoryService)
    {
        _subcategoryService = subcategoryService;
    }

    /// <summary>
    ///     Получить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории <see cref="SubCategoryInfoDto" />.</returns>
    /// <response code="200">Подкатегории найдена.</response>
    /// <response code="404">Подкатегории не найдена.</response>
    [HttpGet]
    [ProducesResponseType(typeof(SubCategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var result = await _subcategoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все подкатегории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Подкатегории найдены.</response>
    /// <returns>Массив подкатегорий <see cref="SubCategoryDto" /></returns>
    [ProducesResponseType(typeof(SubCategoryDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _subcategoryService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Обновить подкатегорию по идентификатору.
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
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([Required] Guid id, SubCategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _subcategoryService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Подкатегория успешно удалена.</response>
    /// <response code="404">Подкатегория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _subcategoryService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}