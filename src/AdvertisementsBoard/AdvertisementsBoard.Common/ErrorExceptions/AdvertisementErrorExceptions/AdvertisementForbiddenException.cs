namespace AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;

/// <summary>
///     Исключение, когда доступ на изменение объявления запрещен.
/// </summary>
public class AdvertisementForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AdvertisementForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    public AdvertisementForbiddenException() : base("Нет прав на изменение объявления.")
    {
    }
}