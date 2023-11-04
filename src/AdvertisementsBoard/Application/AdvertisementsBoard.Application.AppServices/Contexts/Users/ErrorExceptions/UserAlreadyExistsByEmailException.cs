namespace AdvertisementsBoard.Application.AppServices.Contexts.Users.ErrorExceptions;

/// <summary>
///     Исключение, когда пользователь уже существует с таким адресом электронной почты.
/// </summary>
public class UserAlreadyExistsByEmailException : Exception
{
    /// <summary>
    ///     Инициализирует экземпляр класса <see cref="UserAlreadyExistsByEmailException" /> с указанием сообщения об ошибке.
    /// </summary>
    public UserAlreadyExistsByEmailException() : base("Такой адрес электронной почты уже используется.")
    {
    }
}