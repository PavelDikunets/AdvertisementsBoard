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
        CreateMap<Account, AccountDto>();
        CreateMap<Account, AccountInfoDto>();
        CreateMap<Account, AccountShortInfoDto>();
        CreateMap<Account, AccountCreatedDto>();
        CreateMap<Account, AccountAdminDto>();
        
        CreateMap<AccountCreateDto, Account>().IgnoreAllNonExisting();
        CreateMap<AccountPasswordEditDto, Account>().IgnoreAllNonExisting();
        CreateMap<AccountBlockStatusDto, Account>().IgnoreAllNonExisting();
    }
}