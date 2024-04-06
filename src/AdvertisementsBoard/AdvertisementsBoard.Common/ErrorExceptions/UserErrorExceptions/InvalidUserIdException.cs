namespace AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

public class InvalidUserIdException : Exception
{
    public InvalidUserIdException(string userIdString)
        : base($"Недействительный идентификатор пользователя: {userIdString}")
    {
    }
}