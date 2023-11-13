using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с категорями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("categories")]
[Authorize(Policy = "NotBlocked")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryController" />
    /// </summary>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    public CategoryController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    /// <summary>
    ///     Получить все категории.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Категории найдены.</response>
    /// <returns>Список категорий с краткой информацией.</returns>
    [ProducesResponseType(typeof(List<CategoryShortInfoDto>), StatusCodes.Status200OK)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetAllAsync(cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать новую категорию.
    /// </summary>
    /// <param name="dto">Модель создания категории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Категория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="409">Категория с таким именем уже существует.</response>
    /// <response code="401">Аутентификация не выполнена.</response>
    /// <returns>Идентификатор созданной категории.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAsync(CategoryCreateDto dto, CancellationToken cancellationToken)
    {
        var id = await _categoryService.CreateAsync(dto, cancellationToken);
        return Created(nameof(CreateAsync), id);
    }

    /// <summary>
    ///     Получить категорию.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель категории.</returns>
    /// <response code="200">Категория найдена.</response>
    /// <response code="404">Категория не найдена.</response>
    [ProducesResponseType(typeof(CategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _categoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }


    /// <summary>
    ///     Редактировать категорию.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель редактирования категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Категория не найдена.</response>
    /// <response code="200">Категория успешно обновлена.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель с обновленной информацией о категории.</returns>
    [ProducesResponseType(typeof(CategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, CategoryEditDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _categoryService.UpdateByIdAsync(id, dto, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Удалить категорию.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Категория успешно удалена.</response>
    /// <response code="404">Категория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        await _categoryService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}