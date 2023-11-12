namespace AdvertisementsBoard.Common.ErrorExceptions.PasswordErrorExceptions;

/// <summary>
///     Исключение, когда текущий пароль неверный.
/// </summary>
public class IncorrectCurrentPasswordException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="IncorrectCurrentPasswordException" /> с указанием сообщения об ошибке.
    /// </summary>
    public IncorrectCurrentPasswordException() : base("Текущий пароль неверный.")
    {
    }
}