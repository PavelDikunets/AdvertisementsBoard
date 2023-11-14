namespace AdvertisementsBoard.Contracts.Comments;

/// <summary>
///     Модель комментария.
/// </summary>
public class CommentDto
{
    /// <summary>
    ///     Текст комментария.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Идентификатор объявления.
    /// </summary>
    public Guid AdvertisementId { get; set; }

    /// <summary>
    ///     Идентификатор пользователя.
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    ///     Дата создания комментария.
    /// </summary>
    public DateTime Created { get; set; }
}