using System.ComponentModel.DataAnnotations;
using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Common.ErrorExceptions;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.SubCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace AdvertisementsBoard.Hosts.Api.Controllers;

/// <summary>
///     Контроллер для работы с подкатегорями.
/// </summary>
/// <response code="500">Произошла внутренняя ошибка на стороне сервера.</response>
[ApiController]
[Route("subcategories")]
[Authorize(Policy = "NotBlocked")]
[ProducesResponseType(typeof(ErrorDto), StatusCodes.Status500InternalServerError)]
public class SubCategoryController : ControllerBase
{
    private readonly ILogger<SubCategoryController> _logger;
    private readonly ISubCategoryService _subCategoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryController" />
    /// </summary>
    /// <param name="subCategoryService">Сервис для работы с подкатегориями.</param>
    /// <param name="logger">Логгер.</param>
    public SubCategoryController(ISubCategoryService subCategoryService, ILogger<SubCategoryController> logger)
    {
        _subCategoryService = subCategoryService;
        _logger = logger;
    }

    /// <summary>
    ///     Получить список подкатегорий в категории по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="200">Подкатегории найдены.</response>
    /// <returns>Список подкатегорий с краткой информацией.</returns>
    [ProducesResponseType(typeof(List<SubCategoryShortInfoDto>), StatusCodes.Status200OK)]
    [HttpGet("/categories/{id:guid}/subcategories")]
    [AllowAnonymous]
    public async Task<IActionResult> GetAllAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос списка подкатегорий в категории Id: '{CategoryId}'.", id);

        var listOfSubcategories = await _subCategoryService.GetAllAsync(id, cancellationToken);

        _logger.LogInformation("Список подкатегорий успешно получен в категории Id: '{CategoryId}'.", id);

        return Ok(listOfSubcategories);
    }

    /// <summary>
    ///     Создать подкатегорию в категории по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="createDto">Модель создания подкатегории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Подкатегория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost("/categories/{id:guid}/subcategories")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAsync([Required] Guid id, SubCategoryCreateDto createDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос создания подкатегории: '{SubCategory}' в категории Id: '{CategoryId}'.",
            JsonConvert.SerializeObject(createDto), id);

        var createdSubCategoryId = await _subCategoryService.CreateAsync(id, createDto, cancellationToken);

        var relativeUrl = Url.Action("GetById", new { id = createdSubCategoryId });

        if (relativeUrl == null) throw new UrlGenerationException();

        _logger.LogInformation(
            "Подкатегория успешно создана Id: '{SubCategoryId}' '{SubCategory}' в категории Id: '{CagerotyId}'.",
            createdSubCategoryId, JsonConvert.SerializeObject(createDto), id);

        return Created(relativeUrl, createdSubCategoryId);
    }

    /// <summary>
    ///     Получить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Модель подкатегории.</returns>
    /// <response code="200">Подкатегории найдена.</response>
    /// <response code="404">Подкатегории не найдена.</response>
    [ProducesResponseType(typeof(SubCategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [HttpGet("{id:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос подкатегории по Id: '{SubCategoryId}'.", id);

        var subCategory = await _subCategoryService.GetByIdAsync(id, cancellationToken);

        _logger.LogInformation("Категория успешно получена по Id: '{SubCategoryId}' '{SubCategory}'.",
            id, JsonConvert.SerializeObject(subCategory));

        return Ok(subCategory);
    }

    /// <summary>
    ///     Обновить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="updateDto">Модель обновления подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="404">Подкатегории не найдена.</response>
    /// <response code="200">Подкатегории успешно обновлена.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Модель с обновленной информацией о подкатегории.</returns>
    [ProducesResponseType(typeof(SubCategoryInfoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> UpdateByIdAsync(Guid id, SubCategoryUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос обновления подкатегории по Id: '{SubCategoryId}.", id);

        var updatedSubCategory = await _subCategoryService.UpdateByIdAsync(id, updateDto, cancellationToken);

        _logger.LogInformation("Подкатегория успешно обновлена по Id: '{SubCategoryId}', '{SubCategory}'.",
            id, JsonConvert.SerializeObject(updatedSubCategory));

        return Ok(updatedSubCategory);
    }

    /// <summary>
    ///     Удалить подкатегорию по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="204">Подкатегория успешно удалена.</response>
    /// <response code="404">Подкатегория не найдена.</response>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Запрос удаления подкатегории по Id: '{SubCategoryId}'.", id);

        await _subCategoryService.DeleteByIdAsync(id, cancellationToken);

        _logger.LogInformation("Подкатегория успешно удалена по Id: '{SubCategoryId}'.", id);

        return NoContent();
    }
}