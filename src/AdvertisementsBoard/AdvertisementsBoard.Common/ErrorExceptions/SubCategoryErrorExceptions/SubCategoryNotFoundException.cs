namespace AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;

/// <summary>
///     Исключение, когда подкатегория не найдена по идентификатору.
/// </summary>
public class SubCategoryNotFoundException : Exception
{
    public SubCategoryNotFoundException() : base(
        "Подкатегория не найдена.")
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="SubCategoryNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    public SubCategoryNotFoundException(Guid subcategoryId) : base(
        $"Подкатегория по идентификатору '{subcategoryId}' не найдена.")
    {
    }
}