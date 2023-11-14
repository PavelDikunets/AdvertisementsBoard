using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CommentErrorExceptions;
using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Domain.Comments;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;

/// <inheritdoc />
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;

    public CommentService(ICommentRepository commentRepository, IMapper mapper)
    {
        _mapper = mapper;
        _commentRepository = commentRepository;
    }

    /// <inheritdoc />
    public async Task<CommentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<CommentInfoDto>(comment);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<CommentInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        var listComments = await _commentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        var commentDtos = _mapper.Map<List<CommentInfoDto>>(listComments);
        return commentDtos;
    }

    /// <inheritdoc />
    public async Task<CommentInfoDto> CreateAsync(Guid advertisementId, Guid userId, CommentCreateDto createDto,
        CancellationToken cancellationToken)
    {
        var comment = _mapper.Map<Comment>(createDto);

        comment.Created = DateTime.UtcNow;
        comment.UserId = userId;
        comment.AdvertisementId = advertisementId;
        var createdComment = await _commentRepository.CreateAsync(comment, cancellationToken);

        var createdDto = _mapper.Map<CommentInfoDto>(createdComment);
        return createdDto;
    }

    /// <inheritdoc />
    public async Task<CommentUpdatedDto> UpdateByIdAsync(Guid id, Guid userId, CommentEditDto editDto,
        CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);

        if (comment.UserId != userId) throw new CommentForbiddenException();

        _mapper.Map(editDto, comment);
        comment.UserId = userId;
        var updatedComment = await _commentRepository.UpdateAsync(comment, cancellationToken);

        var updatedDto = _mapper.Map<CommentUpdatedDto>(updatedComment);
        return updatedDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var comment = await _commentRepository.GetByIdAsync(id, cancellationToken);

        if (comment.UserId != userId) throw new CommentForbiddenException();

        await _commentRepository.DeleteByIdAsync(id, cancellationToken);
    }
}