namespace AdvertisementsBoard.Application.AppServices.Contexts.SubCategories.ErrorExceptions;

/// <summary>
///     Исключение, когда подкатегория уже существует с таким же наименованием.
/// </summary>
public class SubCategoryAlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="SubCategoryAlreadyExistsException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="subCategoryName">Наименование подкатегории.</param>
    public SubCategoryAlreadyExistsException(string subCategoryName) : base(
        $"Подкатегория с наименованием '{subCategoryName}' уже существует в этой категории.")
    {
    }
}