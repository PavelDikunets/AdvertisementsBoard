namespace AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;

/// <summary>
///     Исключение, когда объявление не найдено по идентификатору.
/// </summary>
public class AdvertisementNotFoundException : Exception
{
    public AdvertisementNotFoundException() : base("Объявление не найдено.")
    {
    }

    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AdvertisementNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="advertisementId">Идентификатор объявления.</param>
    public AdvertisementNotFoundException(Guid advertisementId) : base(
        $"Объявление с идентификатором '{advertisementId}' не найдено.")
    {
    }
}