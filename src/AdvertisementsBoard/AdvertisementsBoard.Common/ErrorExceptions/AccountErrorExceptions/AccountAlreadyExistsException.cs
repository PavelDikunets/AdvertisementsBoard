namespace AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;

/// <summary>
///     Исключение, когда такой аккаунт уже существует.
/// </summary>
public class AccountAlreadyExistsException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AccountAlreadyExistsException" /> с указанием сообщения об
    ///     ошибке.
    /// </summary>
    public AccountAlreadyExistsException() : base("Аккаунт уже существует.")
    {
    }
}