namespace AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;

/// <summary>
///     Исключение, когда аккаунт не найден.
/// </summary>
public class AccountNotFoundException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="AccountNotFoundException" /> с указанием сообщения об ошибке.
    /// </summary>
    public AccountNotFoundException() : base("Аккаунт не найден.")
    {
    }

    public AccountNotFoundException(Guid accountId) : base($"Аккаунт по Id: '{accountId}' не найден.")
    {
    }
}