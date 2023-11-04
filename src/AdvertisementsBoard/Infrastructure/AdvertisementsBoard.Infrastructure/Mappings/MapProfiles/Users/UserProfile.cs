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

        CreateMap<UserDto, UserInfoDto>();
        CreateMap<UserDto, UserShortInfoDto>();

        CreateMap<UserUpdateDto, UserDto>().IgnoreAllNonExisting();
        CreateMap<UserCreateDto, UserDto>().IgnoreAllNonExisting();
    }
}