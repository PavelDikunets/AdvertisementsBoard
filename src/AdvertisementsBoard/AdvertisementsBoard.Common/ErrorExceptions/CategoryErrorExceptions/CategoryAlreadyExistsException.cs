namespace AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;

/// <summary>
///     Исключение, когда категория уже существует.
/// </summary>
public class CategoryAlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CategoryAlreadyExistsException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="categoryName">Имя категории.</param>
    public CategoryAlreadyExistsException(string categoryName) : base($"Категория '{categoryName}' уже существует.")
    {
    }
}