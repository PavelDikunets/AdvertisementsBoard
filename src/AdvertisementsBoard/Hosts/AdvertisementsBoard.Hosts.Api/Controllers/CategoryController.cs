using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Contracts.Categories;
using AdvertisementsBoard.Contracts.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
    private readonly ILogger<CategoryController> _logger;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CategoryController" />
    /// </summary>
    /// <param name="categoryService">Сервис для работы с категориями.</param>
    /// <param name="logger">Логгер.</param>
    public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список категорий.
    /// </summary>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Категории найдены.</response>
    /// <returns>Список категорий с краткой информацией.</returns>
    [ProducesResponseType(typeof(List<CategoryShortInfoDto>), StatusCodes.Status200OK)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка категорий.");

        var listOfCategories = await _categoryService.GetAllAsync(cancellationToken);

        _logger.LogInformation("Список категорий успешно получен.");

        return Ok(listOfCategories);
    }

    /// <summary>
    ///     Создать новую категорию.
    /// </summary>
    /// <param name="createDto">Модель создания категории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Категория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <response code="409">Категория с таким именем уже существует.</response>
    /// <response code="401">Аутентификация не выполнена.</response>
    /// <response code="403">Доступ запрещен.</response>
    /// <returns>Модель созданной категории.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status409Conflict)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status403Forbidden)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAsync([FromBody] CategoryCreateDto createDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос создания категории: '{Category}'.", JsonConvert.SerializeObject(createDto));

        var createdCategoryId = await _categoryService.CreateAsync(createDto, cancellationToken);

        var relativeUrl = Url.Action("GetById", new { id = createdCategoryId });

        if (relativeUrl == null) throw new UrlGenerationException();

        _logger.LogInformation("Категория успешно создана Id: '{CategoryId}' '{Category}'.",
            createdCategoryId, JsonConvert.SerializeObject(createDto));

        return Created(relativeUrl, createdCategoryId);
    }

    /// <summary>
    ///     Получить категорию по идентификатору.
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
    public async Task<IActionResult> GetByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос категории по Id: '{CategoryId}'.", id);

        var category = await _categoryService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Категория успешно получена по Id: '{CategoryId}' '{Category}'.",
            id, JsonConvert.SerializeObject(category));

        return Ok(category);
    }

    /// <summary>
    ///     Обновить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="updateDto">Модель редактирования категории.</param>
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
    public async Task<IActionResult> UpdateByIdAsync([Required] Guid id, [FromBody] CategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос обновления категории по Id: '{CategoryId}.", id);

        var updatedCategory = await _categoryService.UpdateByIdAsync(id, updateDto, cancellationToken);

        _logger.LogInformation("Категория успешно обновлена по Id: '{CategoryId}', '{Category}'.",
            id, JsonConvert.SerializeObject(updatedCategory));

        return Ok(updatedCategory);
    }

    /// <summary>
    ///     Удалить категорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Категория успешно удалена.</response>
    /// <response code="404">Категория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteByIdAsync([Required] Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос удаления категории по Id: '{CategoryId}'.", id);

        await _categoryService.DeleteByIdAsync(id, cancellationToken);

        _logger.LogInformation("Категория успешно удалена по Id: '{CategoryId}'.", id);

        return NoContent();
    }
}