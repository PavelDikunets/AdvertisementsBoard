namespace AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;

/// <summary>
///     Исключение, когда категория не найдена по идентификатору.
/// </summary>
public class CategoryNotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CategoryNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    public CategoryNotFoundException() : base("Категория не найдена.")
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CategoryNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    public CategoryNotFoundException(Guid categoryId) : base(
        $"Категория с идентификатором '{categoryId}' не найдена.")
    {
    }
}