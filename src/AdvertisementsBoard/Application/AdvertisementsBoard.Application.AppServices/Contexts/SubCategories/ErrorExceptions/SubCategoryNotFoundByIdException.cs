namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.ErrorExceptions;

/// <summary>
///     Исключение, когда подкатегория не найдена по идентификатору.
/// </summary>
public class SubCategoryNotFoundByIdException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="SubCategoryNotFoundByIdException" /> с указанием сообщения об ошибке.
    /// </summary>
    public SubCategoryNotFoundByIdException(Guid subcategoryId) : base(
        $"Подкатегория по идентификатору '{subcategoryId}' не найдена.")
    {
    }
}