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
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<User, UserShortInfoDto>();
        CreateMap<User, UserUpdatedDto>();

        CreateMap<User, UserInfoDto>();
        CreateMap<UserDto, UserShortInfoDto>();

        CreateMap<User, UserCreatedDto>();
        CreateMap<UserRoleDto, UserDto>().IgnoreAllNonExisting().ReverseMap();
        CreateMap<UserEditDto, UserDto>().IgnoreAllNonExisting();
        CreateMap<UserCreateDto, UserDto>().IgnoreAllNonExisting();
    }
}