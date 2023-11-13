namespace AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

/// <summary>
///     Исключение, когда пользователь не найден.
/// </summary>
public class UserNotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса
    ///     <see cref="UserNotFoundException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    public UserNotFoundException() : base("Пользователь не найден.")
    {
    }

    public UserNotFoundException(Guid userId) : base($"Пользователь c Id: '{userId}' не найден.")
    {
    }
}