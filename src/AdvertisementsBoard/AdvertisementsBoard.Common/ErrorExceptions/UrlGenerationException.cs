namespace AdvertisementsBoard.Common.ErrorExceptions;

/// <summary>
///     Исключение, когда невозможно сгенерировать Url.
/// </summary>
public class UrlGenerationException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса
    ///     <see cref="UrlGenerationException" /> с указанием сообщения об ошибке.
    /// </summary>
    public UrlGenerationException() : base("Невозможно сгенерировать URL.")
    {
    }
}