namespace AdvertisementsBoard.Application.AppServices.Contexts.Advertisements.ErrorExceptions;

/// <summary>
///     Исключение, когда объявление не найдено по идентификатору.
/// </summary>
public class AdvertisementNotFoundByIdException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AdvertisementNotFoundByIdException" /> с указанием сообщения об ошибке.
    /// </summary>
    /// <param name="advertisementId">Идентификатор объявления.</param>
    public AdvertisementNotFoundByIdException(Guid advertisementId) : base(
        $"Объявление с идентификатором '{advertisementId}' не найдено.")
    {
    }
}