namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.ErrorExceptions;

/// <summary>
///     Исключение, когда категория не найдена по идентификатору.
/// </summary>
public class CategoryNotFoundByIdException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CategoryNotFoundByIdException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="categoryId">Идентификатор категории.</param>
    public CategoryNotFoundByIdException(Guid categoryId) : base(
        $"Категория с идентификатором '{categoryId}' не найдена.")
    {
    }
}