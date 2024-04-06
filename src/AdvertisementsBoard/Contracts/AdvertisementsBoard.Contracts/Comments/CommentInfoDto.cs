namespace AdvertisementsBoard.Contracts.Comments;

/// <summary>
///     Модель комментария.
/// </summary>
public class CommentInfoDto
{
    /// <summary>
    ///     Текст комментария.
    /// </summary>
    public string Text { get; set; }

    /// <summary>
    ///     Дата создания комментария.
    /// </summary>
    public DateTime Created { get; set; }
}