namespace AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

/// <summary>
///     Исключение, когда доступ на изменение пользователя запрещен.
/// </summary>
public class UserForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="UserForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    public UserForbiddenException() : base("Нет прав на изменение этого пользователя.")
    {
    }
}