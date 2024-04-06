namespace AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

/// <summary>
///     Исключение, когда пользователь уже существует с таким никнеймом.
/// </summary>
public class UserAlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="UserAlreadyExistsException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    public UserAlreadyExistsException(string nickName) : base($"Пользователь '{nickName}' уже существует.")
    {
    }
}