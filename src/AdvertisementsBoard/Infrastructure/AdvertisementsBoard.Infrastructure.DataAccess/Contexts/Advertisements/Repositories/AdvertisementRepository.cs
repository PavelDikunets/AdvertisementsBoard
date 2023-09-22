﻿using AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.Repositories;
using AdvertisementsBoard.Contracts.Advertisements;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Advertisements.Repositories;

/// <summary>
///     Репозиторий объявлений.
/// </summary>
public class AdvertisementRepository : IAdvertisementRepository
{
    private readonly IBaseDbRepository<Advertisement> _repository;

    public AdvertisementRepository(IBaseDbRepository<Advertisement> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAll().Where(s => s.Id == id)
            .Select(d => new AdvertisementInfoDto
            {
                Title = d.Title,
                Description = d.Description,
                Price = d.Price,
                TagNames = d.TagNames,
                IsActive = d.IsActive,
                Attachments = d.Attachments.Select(att => new AttachmentInfoDto
                {
                    FileName = att.FileName
                }).ToList()
            })
            .FirstOrDefaultAsync(cancellationToken);
        if (dto == null) throw new Exception("Объявление не найдено");

        return dto;
    }

    /// <inheritdoc />
    public async Task<AdvertisementShortInfoDto[]> GetAllAsync(CancellationToken cancellationToken)
    {
        var entities = _repository.GetAll();

        var models = entities.Select(e => new AdvertisementShortInfoDto
        {
            Title = e.Title,
            Price = e.Price
        });

        return await models.ToArrayAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Advertisement entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AdvertisementInfoDto> UpdateAsync(Advertisement currentEntity,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(currentEntity.Id, cancellationToken);
        entity.Title = currentEntity.Title;
        entity.Description = currentEntity.Description;
        entity.Price = currentEntity.Price;
        entity.TagNames = currentEntity.TagNames;
        entity.IsActive = currentEntity.IsActive;

        await _repository.UpdateAsync(entity, cancellationToken);

        var model = new AdvertisementInfoDto
        {
            Title = entity.Title,
            Description = entity.Description,
            Price = entity.Price,
            TagNames = entity.TagNames,
            IsActive = entity.IsActive
        };
        return model;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
        return true;
    }
}