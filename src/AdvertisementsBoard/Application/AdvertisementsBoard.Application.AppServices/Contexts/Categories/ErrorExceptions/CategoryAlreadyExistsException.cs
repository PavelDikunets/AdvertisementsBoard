namespace AdvertisementsBoard.Application.AppServices.Contexts.Categories.ErrorExceptions;

/// <summary>
///     Исключение, когда категория уже существует с таким наименованием.
/// </summary>
public class CategoryAlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="CategoryAlreadyExistsException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="сategoryName">Наименование категории.</param>
    public CategoryAlreadyExistsException(string сategoryName) : base(
        $"Категория с наименованием '{сategoryName}' уже существует.")
    {
    }
}