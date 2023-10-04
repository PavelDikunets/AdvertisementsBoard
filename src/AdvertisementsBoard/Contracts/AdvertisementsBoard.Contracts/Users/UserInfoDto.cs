namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
/// Модель информации о пользователе.
/// </summary>
public class UserInfoDto
{
    /// <summary>
    /// Имя пользователя.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
}