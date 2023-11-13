using System.Linq.Expressions;
using AdvertisementsBoard.Application.AppServices.Contexts.Attachments.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;
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
        var attachment = await TryGetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AttachmentDto>(attachment);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AttachmentShortInfoDto>> GetAllByAdvertisementIdAsync(Guid id,
        CancellationToken cancellationToken)
    {
        var listAttachments = await _repository.GetAll()
            .Where(a => a.AdvertisementId == id)
            .ProjectTo<AttachmentShortInfoDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return listAttachments;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(AttachmentDto dto, CancellationToken cancellationToken)
    {
        var entity = _mapper.Map<Attachment>(dto);

        await _repository.AddAsync(entity, cancellationToken);
        return entity.Id;
    }

    /// <inheritdoc />
    public async Task<AttachmentDto> UpdateAsync(AttachmentDto dto,
        CancellationToken cancellationToken)
    {
        var attachment = _mapper.Map<Attachment>(dto);

        await _repository.UpdateAsync(attachment, cancellationToken);

        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var attachment = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(attachment, cancellationToken);
    }

    public async Task<AttachmentDto> FindWhereAsync(Expression<Func<Attachment, bool>> filter,
        CancellationToken cancellationToken)
    {
        var attachment = await _repository.GetAllFiltered(filter)
            .ProjectTo<AttachmentDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        if (attachment == null) throw new AttachmentNotFoundException();

        return attachment;
    }

    private async Task<Attachment> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _repository.GetByIdAsync(id, cancellationToken);

        if (account == null) throw new AttachmentNotFoundException(id);

        return account;
    }
}