using AdvertisementsBoard.Contracts.Accounts;
using AdvertisementsBoard.Domain.Accounts;
using AutoMapper;

namespace AdvertisementsBoard.Infrastructure.Mappings.MapProfiles.Accounts;

/// <summary>
///     Профиль маппера для аккаунтов.
/// </summary>
public class AccountProfile : Profile
{
    /// <summary>
    ///     Конструктор, настраивающий маппинг между моделями AdvertisementDtos и Advertisement.
    /// </summary>
    public AccountProfile()
    {
        CreateMap<AccountSignUpDto, AccountDto>().IgnoreAllNonExisting();
        CreateMap<AccountPasswordEditDto, AccountDto>().IgnoreAllNonExisting();
        CreateMap<AccountBlockDto, AccountDto>().IgnoreAllNonExisting();

        CreateMap<AccountDto, AccountInfoDto>();
        CreateMap<Account, AccountShortInfoDto>();
        CreateMap<Account, AccountCreatedDto>();
        CreateMap<Account, AccountDto>().ReverseMap();
    }
}