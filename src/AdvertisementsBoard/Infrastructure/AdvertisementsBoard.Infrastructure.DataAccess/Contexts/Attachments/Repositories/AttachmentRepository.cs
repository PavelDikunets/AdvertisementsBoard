using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Contracts.Attachments;
using AdvertisementsBoard.Domain.Attachments;
using AdvertisementsBoard.Infrastructure.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Attachments.Repositories;

/// <summary>
///     Репозиторий вложений.
/// </summary>
public class AttachmentRepository : IAttachmentRepository
{
    private readonly IMapper _mapper;
    private readonly IBaseDbRepository<Attachment> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AttachmentRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий вложений.</param>
    /// <param name="mapper">Маппер.</param>
    public AttachmentRepository(IBaseDbRepository<Attachment> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AttachmentDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAll()
            .ProjectTo<AttachmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto[]> GetAllByAdvertisementIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .Where(a => a.AdvertisementId == id)
            .ProjectTo<AttachmentShortInfoDto>(_mapper.ConfigurationProvider)
            .ToArrayAsync(cancellationToken);

        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Attachment entity, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AttachmentShortInfoDto> UpdateByIdAsync(Attachment updatedEntity,
        CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(updatedEntity, cancellationToken);

        var dto = _mapper.Map<AttachmentShortInfoDto>(updatedEntity);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<bool> TryFindByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetAll().AnyAsync(a => a.Id == id, cancellationToken);
        return result;
    }
}