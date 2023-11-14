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
        CreateMap<Account, AccountInfoDto>();
        CreateMap<Account, AccountBlockStatusDto>();
        CreateMap<Account, AccountDto>();
        CreateMap<Account, AccountCreatedDto>().IgnoreAllNonExisting();
        CreateMap<AccountCreateDto, Account>().IgnoreAllNonExisting();
    }
}