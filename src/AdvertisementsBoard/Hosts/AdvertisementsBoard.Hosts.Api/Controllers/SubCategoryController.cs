using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;
using AdvertisementsBoard.Contracts.Errors;
using AdvertisementsBoard.Contracts.SubCategories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    ///     Получить все подкатегории в категории.
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
        var result = await _subCategoryService.GetAllAsync(id, cancellationToken);
        return Ok(result);
    }

    /// <summary>
    ///     Создать подкатегорию в категории.
    /// </summary>
    /// <param name="id">Идентификатор категории.</param>
    /// <param name="dto">Модель создания подкатегории</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <response code="201">Подкатегория успешно создана.</response>
    /// <response code="400">Некорректный запрос.</response>
    /// <returns>Идентификатор созданной подкатегории.</returns>
    [ProducesResponseType(typeof(ErrorDto), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(Guid), StatusCodes.Status201Created)]
    [HttpPost("/categories/{id:guid}/subcategories")]
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> CreateAsync(Guid id, SubCategoryCreateDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _subCategoryService.CreateAsync(id, dto, cancellationToken);
        return Created(nameof(CreateAsync), result);
    }

    /// <summary>
    ///     Получить подкатегорию.
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
        var result = await _subCategoryService.GetByIdAsync(id, cancellationToken);
        return Ok(result);
    }


    /// <summary>
    ///     Редактировать подкатегорию.
    /// </summary>
    /// <param name="id">Идентификатор подкатегории.</param>
    /// <param name="dto">Модель редактирования подкатегории.</param>
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
    public async Task<IActionResult> UpdateByIdAsync(Guid id, SubCategoryEditDto dto,
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
    [Authorize(Roles = "Administrator")]
    public async Task<IActionResult> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subCategoryService.DeleteByIdAsync(id, cancellationToken);
        return NoContent();
    }
}