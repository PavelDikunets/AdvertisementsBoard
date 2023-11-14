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
        CreateMap<UserDto, UserSignInDto>();
        CreateMap<UserDto, UserInfoDto>();
        CreateMap<User, UserCreatedDto>();
        CreateMap<User, UserDto>().ReverseMap();

        CreateMap<UserEditDto, User>().IgnoreAllNonExisting();
        CreateMap<UserRoleDto, User>().IgnoreAllNonExisting();
        CreateMap<UserCreateDto, User>().IgnoreAllNonExisting();
    }
}