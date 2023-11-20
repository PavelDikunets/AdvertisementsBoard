using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Contracts.Comments;
using AdvertisementsBoard.Domain.Comments;
using AutoMapper;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Comments.Services;

/// <inheritdoc />
public class CommentService : ICommentService
{
    private readonly ICommentRepository _commentRepository;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CommentService" />.
    /// </summary>
    /// <param name="commentRepository">Репозиторий для работы с комментариями.</param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    /// <param name="mapper">Маппер.</param>
    public CommentService(ICommentRepository commentRepository, IMapper mapper, IUserService userService)
    {
        _commentRepository = commentRepository;
        _userService = userService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<CommentInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var commentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);

        var commentDto = _mapper.Map<CommentInfoDto>(commentEntity);
        return commentDto;
    }

    /// <inheritdoc />
    public async Task<List<CommentInfoDto>> GetAllByAdvertisementIdAsync(Guid advertisementId,
        CancellationToken cancellationToken)
    {
        var commentEntities = await _commentRepository.GetAllByAdvertisementIdAsync(advertisementId, cancellationToken);

        var commentDtos = _mapper.Map<List<CommentInfoDto>>(commentEntities);
        return commentDtos;
    }

    /// <inheritdoc />
    public async Task<Guid> CreateAsync(Guid advertisementId, Guid userId, CommentCreateDto createDto,
        CancellationToken cancellationToken)
    {
        var newCommentEntity = _mapper.Map<Comment>(createDto);

        newCommentEntity.Created = DateTime.UtcNow;
        newCommentEntity.UserId = userId;
        newCommentEntity.AdvertisementId = advertisementId;

        var createdCommentId = await _commentRepository.CreateAsync(newCommentEntity, cancellationToken);
        return createdCommentId;
    }

    /// <inheritdoc />
    public async Task<CommentUpdatedDto> UpdateByIdAsync(Guid id, Guid userId, CommentUpdateDto updateDto,
        CancellationToken cancellationToken)
    {
        var commentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);

        await _userService.CheckUserPermissionAsync(userId, commentEntity.UserId, cancellationToken);

        _mapper.Map(updateDto, commentEntity);
        commentEntity.Id = id;
        commentEntity.UserId = userId;

        var updatedCommentEntity = await _commentRepository.UpdateAsync(commentEntity, cancellationToken);

        var updatedCommentDto = _mapper.Map<CommentUpdatedDto>(updatedCommentEntity);
        return updatedCommentDto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        var commentEntity = await _commentRepository.GetByIdAsync(id, cancellationToken);

        await _userService.CheckUserPermissionAsync(userId, commentEntity.UserId, cancellationToken);

        await _commentRepository.DeleteByIdAsync(id, cancellationToken);
    }
}