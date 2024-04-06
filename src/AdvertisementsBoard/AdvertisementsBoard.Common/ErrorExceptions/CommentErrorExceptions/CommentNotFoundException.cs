namespace AdvertisementsBoard.Common.ErrorExceptions.CommentErrorExceptions;

/// <summary>
///     Исключение, когда комментарий не найден по идентификатору.
/// </summary>
public class CommentNotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CommentNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    public CommentNotFoundException() : base("Комментарий не найден.")
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CommentNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="commentId">Идентификатор вложения.</param>
    public CommentNotFoundException(Guid commentId) : base(
        $"Комментарий с идентификатором '{commentId}' не найден.")
    {
    }
}