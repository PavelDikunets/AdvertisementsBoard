using AdvertisementsBoard.Application.AppServices.Contexts.Comments.Repositories;
using AdvertisementsBoard.Common.ErrorExceptions.CommentErrorExceptions;
using AdvertisementsBoard.Domain.Comments;
using AdvertisementsBoard.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementsBoard.Infrastructure.DataAccess.Contexts.Comments.Repositories;

/// <summary>
///     Репозиторий комментариев.
/// </summary>
public class CommentRepository : ICommentRepository
{
    private readonly IBaseDbRepository<Comment> _repository;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="CommentRepository" />
    /// </summary>
    /// <param name="repository">Репозиторий комментариев.</param>
    public CommentRepository(IBaseDbRepository<Comment> repository)
    {
        _repository = repository;
    }

    /// <inheritdoc />
    public async Task<Comment> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await TryGetByIdAsync(id, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<List<Comment>> GetAllByAdvertisementIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var listComments = await _repository.GetAll()
            .Where(a => a.AdvertisementId == id)
            .ToListAsync(cancellationToken);

        return listComments;
    }

    /// <inheritdoc />
    public async Task<Comment> CreateAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _repository.AddAsync(comment, cancellationToken);
        return comment;
    }

    /// <inheritdoc />
    public async Task<Comment> UpdateAsync(Comment comment, CancellationToken cancellationToken)
    {
        await _repository.UpdateAsync(comment, cancellationToken);
        return comment;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await TryGetByIdAsync(id, cancellationToken);

        await _repository.DeleteAsync(comment, cancellationToken);
    }

    private async Task<Comment> TryGetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var comment = await _repository.GetByIdAsync(id, cancellationToken);

        if (comment == null) throw new CommentNotFoundException(id);
        return comment;
    }
}