using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.SubCategories;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с категорями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("[controller]")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly ISubCategoryService _subCategoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryController" />
    /// </summary>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    /// <param name="subCategoryService">Сервис для работы с подкатегориями.</param>
    public CategoryController(ICategoryService categoryService, ISubCategoryService subCategoryService)
    {
        _categoryService = categoryService;
        _subCategoryService = subCategoryService;
    }

    /// <summary>
    ///     Получить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель категории <see cref="CategoryInfoDto" />.</returns>
    /// <response code="200">Категория найдена.</response>
    /// <response code="404">Категория не найдена.</response>
    [HttpGet]
    [ProducesResponseType(typeof(CategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Категории найдены.</response>
    /// <returns>Массив категорий <see cref="CategoryInfoDto" /></returns>
    [ProducesResponseType(typeof(CategoryInfoDto[]), StatusCodes.Status200OK)]
    [HttpGet("Get-all")]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Категория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданной категории <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        var result = await _categoryService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Обновить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель обновления категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Категория не найдена.</response>
    /// <response code="200">Категория успешно обновлена.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель обновления категории <see cref="CategoryUpdateDto" />.</returns>
    [ProducesResponseType(typeof(CategoryUpdateDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut]
    public async Task<IActionResult> UpdateByIdAsync([Required] Guid id, CategoryUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Категория успешно удалена.</response>
    /// <response code="404">Категория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }

    /// <summary>
    ///     Создать подкатегорию по идентификатору категории.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель создания подкатегории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Подкатегория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданной подкатегории <see cref="Guid" />.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost("{id}/SubCategory")]
    public async Task<IActionResult> CreateAsync(Guid id, SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.CreateByCategoryIdAsync(id, dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }
}