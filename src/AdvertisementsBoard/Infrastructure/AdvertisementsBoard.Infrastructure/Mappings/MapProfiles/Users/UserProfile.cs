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
    ///     Конструктор, настраивающий маппинг между моделями UserDtos и User.
    /// </summary>
    public UserProfile()
    {
        CreateMap<UserUpdateDto, UserDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .IgnoreAllNonExisting();

        CreateMap<UserCreateDto, UserDto>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
            .IgnoreAllNonExisting();

        CreateMap<UserCreateDto, User>()
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
            .IgnoreAllNonExisting();

        CreateMap<User, UserShortInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name));

        CreateMap<UserDto, UserInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email));

        CreateMap<User, UserInfoDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email));

        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Id, map => map.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, map => map.MapFrom(src => src.Name))
            .ForMember(dest => dest.Email, map => map.MapFrom(src => src.Email))
            .ForMember(dest => dest.PasswordHash, map => map.MapFrom(src => src.PasswordHash))
            .ReverseMap();
    }
}