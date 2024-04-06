using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Domain.Users;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Users;

/// <summary>
///     Профиль маппера для пользователей.
/// </summary>
public class UserProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями для пользователей.
    /// </summary>
    public UserProfile()
    {
        CreateMap<User, UserShortInfoDto>();
        CreateMap<User, UserUpdatedDto>();
        CreateMap<User, UserInfoDto>();
        CreateMap<User, UserDto>();
        CreateMap<UserInfoDto, UserSignInDto>();
        CreateMap<User, UserRoleDto>();

        CreateMap<UserUpdateDto, User>().IgnoreAllNonExisting();
        CreateMap<UserRoleDto, User>().IgnoreAllNonExisting();
        CreateMap<UserCreateDto, User>().IgnoreAllNonExisting();
    }
}