namespace AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;

/// <summary>
///     Исключение, когда доступ, связанный с аккаунтом запрещен.
/// </summary>
public class AccountForbiddenException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AccountForbiddenException" /> с указанием сообщения об ошибке.
    /// </summary>
    public AccountForbiddenException() : base("Доступ запрещен.")
    {
    }
}