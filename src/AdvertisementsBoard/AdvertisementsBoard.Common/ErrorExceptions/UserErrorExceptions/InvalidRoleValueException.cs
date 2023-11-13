namespace AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

/// <summary>
///     Исключение, когда установлено недопустимое значение для роли.
/// </summary>
public class InvalidRoleValueException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса
    ///     <see cref="InvalidRoleValueException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    public InvalidRoleValueException() : base("Недопустимое значение роли.")
    {
    }
}