namespace AdvertisementsBoard.Common.ErrorExceptions;

/// <summary>
///     Исключение, когда нет разрешений.
/// </summary>
public class PermissionException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="PermissionException" /> с указанием сообщения об ошибке.
    /// </summary>
    public PermissionException() : base("Нет разрешений.")
    {
    }
}