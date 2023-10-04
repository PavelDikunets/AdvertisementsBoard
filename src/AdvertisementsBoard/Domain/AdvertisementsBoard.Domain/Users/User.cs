using AdvertisementsBoard.Domain.Advertisements;
using AdvertisementsBoard.Domain.Base;

namespace AdvertisementsBoard.Domain.Users;

/// <summary>
/// Сущность пользователя.
/// </summary>
public class User : BaseEntity
{
    /// <summary>
    /// Имя пользователя. 
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string Email { get; set; }
    
    /// <summary>
    /// Пароль.
    /// </summary>
    public string Password { get; set; }
    
    /// <summary>
    /// Список объявлений.
    /// </summary>
    public virtual List<Advertisement> Advertisements { get; set; }
}