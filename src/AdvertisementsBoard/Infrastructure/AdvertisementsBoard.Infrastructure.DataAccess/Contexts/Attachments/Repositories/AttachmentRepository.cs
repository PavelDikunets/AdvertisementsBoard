using System.Linq.Expressions;
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
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AttachmentDto>(entity);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var dtos = await _repository.GetAll()
            .Where(a => a.AdvertisementId == id)
            .ProjectTo<AttachmentShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        return dtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Attachment>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AttachmentUpdatedDto> UpdateAsync(AttachmentDto dto,
        CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Attachment>(dto);

        await _repository.UpdateAsync(entity, cancellationToken);

        var updatedDto = _mapper.Map<AttachmentUpdatedDto>(entity);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);
        await _repository.DeleteAsync(entity, cancellationToken);
    }

    public async Task<AttachmentDto> GetWhereAsync(Expression<Func<Attachment, bool>> filter,
        CancellationToken cancellationToken)
    {
        var dto = await _repository.GetAllFiltered(filter)
            .ProjectTo<AttachmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return dto;
    }
}