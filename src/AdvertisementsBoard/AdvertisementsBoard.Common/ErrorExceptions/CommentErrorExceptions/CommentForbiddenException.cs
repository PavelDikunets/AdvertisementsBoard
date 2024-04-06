namespace AdvertisementsBoard.Common.ErrorExceptions.CommentErrorExceptions;

/// <summary>
///     Исключение, когда доступ для комментариев запрещен.
/// </summary>
public class CommentForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CommentForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    public CommentForbiddenException() : base("Нет прав на доступ к комментариям.")
    {
    }
}