namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.ErrorExceptions;

/// <summary>
///     Исключение, когда пользователь не найден по идентификатору.
/// </summary>
public class UserNotFoundByIdException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса
    ///     <see cref="UserNotFoundByIdException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    /// <param name="userId">Идентификатор пользователя.</param>
    public UserNotFoundByIdException(Guid userId) : base($"Пользователь с идентификатором '{userId}' не найден.")
    {
    }
}