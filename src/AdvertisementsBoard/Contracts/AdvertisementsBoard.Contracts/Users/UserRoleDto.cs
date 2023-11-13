using AdvertisementsBoard.Common.Enums.Users;

namespace AdvertisementsBoard.Contracts.Users;

/// <summary>
///     Модель ролей пользователя.
/// </summary>
public class UserRoleDto
{
    /// <summary>
    ///     Перечисление ролей.
    ///     User = 0 - Пользователь
    ///     Moderator = 1 - Модератор
    ///     Administrator = 2 - Администратор
    /// </summary>
    public UserRole Role { get; set; }
}