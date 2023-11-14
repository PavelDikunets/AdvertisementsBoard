using System.ComponentModel.DataAnnotations;

namespace AdvertisementsBoard.Contracts.Comments;

/// <summary>
///     Модель создания комментария.
/// </summary>
public class CommentCreateDto
{
    /// <summary>
    ///     Текст комментария.
    /// </summary>
    [Required(ErrorMessage = "Поле {0} не может быть пустым.")]
    [StringLength(100, MinimumLength = 4, ErrorMessage = "Поле {0} должно содержать от {2} до {1} символов.")]
    public string Text { get; set; }
}