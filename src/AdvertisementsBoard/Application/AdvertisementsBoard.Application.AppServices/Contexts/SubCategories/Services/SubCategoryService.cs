using AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;
using AdvertisementsBoard.Contracts.SubCategories;
using AdvertisementsBoard.Domain.SubCategories;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.Services;

/// <inheritdoc />
public class SubCategoryService : ISubCategoryService
{
    private readonly IMapper _mapper;
    private readonly ISubCategoryRepository _subCategoryRepository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="SubCategoryService" />
    /// </summary>
    /// <param name="subCategoryRepository">Репозиторий для работы с подкатегориями.</param>
    /// <param name="mapper">Маппер.</param>
    public SubCategoryService(ISubCategoryRepository subCategoryRepository, IMapper mapper)
    {
        _subCategoryRepository = subCategoryRepository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var subCategory = await _subCategoryRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<SubCategoryInfoDto>(subCategory);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<SubCategoryShortInfoDto>> GetAllAsync(Guid id, CancellationToken cancellationToken)
    {
        var listSubCategories = await _subCategoryRepository.GetAllAsync(id, cancellationToken);

        var subCategoryDtos = _mapper.Map<List<SubCategoryShortInfoDto>>(listSubCategories);
        return subCategoryDtos;
    }

    /// <inheritdoc />
    public async Task<SubCategoryShortInfoDto> CreateAsync(Guid categoryId, SubCategoryCreateDto createDto,
        CancellationToken cancellationToken)
    {
        await DoesSubCategoryExistByNameAsync(categoryId, createDto.Name, cancellationToken);

        var subCategory = _mapper.Map<SubCategory>(createDto);
        subCategory.CategoryId = categoryId;

        var createdSubCategory = await _subCategoryRepository.CreateAsync(subCategory, cancellationToken);

        var dto = _mapper.Map<SubCategoryShortInfoDto>(createdSubCategory);
        return dto;
    }

    /// <inheritdoc />
    public async Task<SubCategoryInfoDto> UpdateByIdAsync(Guid categoryId, SubCategoryEditDto editDto,
        CancellationToken cancellationToken)
    {
        var currentSubCategory =
            await _subCategoryRepository.FindWhereAsync(s => s.Id == categoryId, cancellationToken);

        await DoesSubCategoryExistByNameAsync(categoryId, editDto.Name, cancellationToken);

        _mapper.Map(editDto, currentSubCategory);

        var updatedSubCategory = await _subCategoryRepository.UpdateAsync(currentSubCategory, cancellationToken);

        var updatedDto = _mapper.Map<SubCategoryInfoDto>(updatedSubCategory);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _subCategoryRepository.DeleteByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DoesSubCategoryExistByIdAsync(Guid subCategoryId, CancellationToken cancellationToken)
    {
        var exist = await _subCategoryRepository.DoesSubCategoryExistWhereAsync(u => u.Id == subCategoryId,
            cancellationToken);

        if (!exist) throw new SubCategoryNotFoundException(subCategoryId);
    }


    private async Task DoesSubCategoryExistByNameAsync(Guid categoryId, string subCategoryName,
        CancellationToken cancellationToken)
    {
        var exist = await _subCategoryRepository.DoesSubCategoryExistWhereAsync(
            a => a.CategoryId == categoryId && a.Name == subCategoryName, cancellationToken);

        if (exist) throw new SubCategoryAlreadyExistsException(subCategoryName);
    }
}