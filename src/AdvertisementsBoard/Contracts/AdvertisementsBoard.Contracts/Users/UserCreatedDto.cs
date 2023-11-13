using AdvertisementsBoard.Contracts.Base;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель созданного пользователя.
/// </summary>
public class UserCreatedDto : BaseDto
{
    /// <summary>
    ///     Имя пользователя.
    /// </summary>
    public string NickName { get; set; }
}