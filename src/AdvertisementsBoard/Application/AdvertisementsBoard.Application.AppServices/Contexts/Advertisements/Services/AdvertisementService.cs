using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Categories.Services;
using AdvertisementsBoard.Application.AppServices.ErrorExceptions;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Advertisements;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Services;

/// <inheritdoc />
public class AdvertisementService : IAdvertisementService
{
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly ICategoryService _categoryService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AdvertisementService" />.
    /// </summary>
    /// <param name="advertisementRepository">Репозиторий для работы с объявлениями.</param>
    /// <param name="categoryService"></param>
    public AdvertisementService(IAdvertisementRepository advertisementRepository,
        ICategoryService categoryService)
    {
        _advertisementRepository = advertisementRepository;
        _categoryService = categoryService;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        var dto = new AdvertisementInfoDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive,
            CategoryId = entity.CategoryId,
            CategoryName = entity.Category.Name,
            SubCategoryId = entity.SubCategoryId,
            SubCategoryName = entity.SubCategory.Name,
            Attachments = entity.Attachments.Select(s => new AttachmentInfoDto
            {
                Url = s.Url
            }).ToList()
        };

        return dto;
    }

    /// <inheritdoc />
    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken, int pageSize,
        int pageNumber)
    {
        var entity = await _advertisementRepository.GetAllAsync(cancellationToken, pageNumber, pageSize);

        var dto = entity.Select(a => new AdvertisementShortInfoDto
        {
            Id = a.Id,
            Title = a.Title,
            Price = a.Price,
            CategoryName = a.Category.Name,
            SubCategoryName = a.SubCategory.Name
        }).ToArray();

        return dto;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Guid categoryId, Guid subCategoryId, AdvertisementCreateDto dto,
        CancellationToken cancellationToken)
    {
        await _categoryService.GetByIdAsync(categoryId, cancellationToken);

        var advertisementEntity = new Advertisement
        {
            Title = dto.Title,
            Description = dto.Description,
            Price = dto.Price,
            TagNames = dto.TagNames,
            IsActive = dto.IsActive,
            CategoryId = categoryId,
            SubCategoryId = subCategoryId
        };

        var id = await _advertisementRepository.CreateAsync(advertisementEntity, cancellationToken);
        return id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementUpdateDto> UpdateByIdAsync(Guid id, AdvertisementUpdateDto dto,
        CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        entity.Title = dto.Title;
        entity.Description = dto.Description;
        entity.Price = dto.Price;
        entity.TagNames = dto.TagNames;
        entity.IsActive = dto.IsActive;


        await _advertisementRepository.UpdateAsync(entity, cancellationToken);

        var updateDto = new AdvertisementUpdateDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive
        };
        return updateDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await FindByIdAsync(id, cancellationToken);

        await _advertisementRepository.DeleteByIdAsync(entity.Id, cancellationToken);
    }


    private async Task<Advertisement> FindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _advertisementRepository.GetByIdAsync(id, cancellationToken);

        if (entity == null) throw new NotFoundException($"Объявление с идентификатором {id} не найдено.");
        return entity;
    }
}