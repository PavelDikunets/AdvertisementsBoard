using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.Services.Passwords.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;
using AdvertisementsBoard.Contracts.Accounts;
using AdvertisementsBoard.Contracts.Users;
using AdvertisementsBoard.Domain.Accounts;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Services;

/// <inheritdoc />
public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IPasswordService _passwordService;
    private readonly IUserService _userService;

    /// <summary>
    ///     Инициализирует экземпляр <see cref="AccountService" />
    /// </summary>
    /// <param name="accountRepository">Репозиторий для работы с аккаунтами.</param>
    /// <param name="mapper">Маппер.</param>
    /// <param name="passwordService">Сервис для работы с паролями.</param>
    /// <param name="configuration"></param>
    /// <param name="userService">Сервис для работы с пользователями.</param>
    public AccountService(IAccountRepository accountRepository, IMapper mapper, IPasswordService passwordService,
        IConfiguration configuration, IUserService userService)
    {
        _accountRepository = accountRepository;
        _passwordService = passwordService;
        _configuration = configuration;
        _userService = userService;
        _mapper = mapper;
    }

    /// <inheritdoc />
    public async Task<AccountAdminDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.GetByIdAsync(id, cancellationToken);

        var accountDto = _mapper.Map<AccountAdminDto>(accountEntity);
        return accountDto;
    }

    /// <inheritdoc />
    public async Task<AccountInfoDto> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.FindWhereAsync(a => a.UserId == userId, cancellationToken);

        var accountDto = _mapper.Map<AccountInfoDto>(accountEntity);
        return accountDto;
    }

    /// <inheritdoc />
    public async Task<List<AccountShortInfoDto>> GetAllAsync(int pageSize, int pageNumber, bool isBlocked,
        CancellationToken cancellationToken)
    {
        var accountEntities = await _accountRepository.GetAllAsync(cancellationToken, pageNumber, pageSize, isBlocked);

        var accountDtos = _mapper.Map<List<AccountShortInfoDto>>(accountEntities);
        return accountDtos;
    }

    /// <inheritdoc />
    public async Task<Guid> SignUpAsync(AccountCreateDto createDto, CancellationToken cancellationToken)
    {
        _passwordService.ComparePasswords(createDto.Password, createDto.ConfirmPassword);

        var accountExist = await _accountRepository.DoesAccountExistWhereAsync(
            a => a.Email == createDto.Email || a.User.NickName == createDto.User.NickName, cancellationToken);

        if (accountExist) throw new AccountAlreadyExistsException();

        var newAccountEntity = _mapper.Map<Account>(createDto);

        newAccountEntity.Created = DateTime.UtcNow;
        newAccountEntity.PasswordHash = _passwordService.HashPassword(createDto.Password);

        var createdAccountId = await _accountRepository.CreateAsync(newAccountEntity, cancellationToken);
        return createdAccountId;
    }

    /// <inheritdoc />
    public async Task<string> SignInAsync(AccountSignInDto signInDto, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.FindWhereAsync(a => a.Email == signInDto.Email, cancellationToken);
        var userDto = await _userService.GetByIdAsync(accountEntity.UserId, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(accountEntity.PasswordHash, signInDto.Password);

        var token = GetJwtToken(accountEntity, userDto);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    /// <inheritdoc />
    public async Task ChangePasswordAsync(Guid userId, AccountPasswordEditDto accountDto,
        CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.FindWhereAsync(a => a.UserId == userId, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(accountEntity.PasswordHash, accountDto.CurrentPassword);

        _passwordService.ComparePasswords(accountDto.NewPassword, accountDto.ConfirmPassword);

        accountEntity.PasswordHash = _passwordService.HashPassword(accountDto.NewPassword);

        var updatedAccount = _mapper.Map(accountDto, accountEntity);

        await _accountRepository.UpdateAsync(updatedAccount, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AccountBlockStatusDto> BlockByIdAsync(Guid id, AccountBlockStatusDto dto,
        CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.GetByIdAsync(id, cancellationToken);

        _mapper.Map(dto, accountEntity);

        await _accountRepository.UpdateAsync(accountEntity, cancellationToken);
        return dto;
    }

    /// <inheritdoc />
    public async Task<bool> IsAccountBlocked(Guid userId, CancellationToken cancellationToken)
    {
        var accountEntity = await _accountRepository.GetByIdAsync(userId, cancellationToken);
        return accountEntity.IsBlocked;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private JwtSecurityToken GetJwtToken(Account account, UserDto userDto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, userDto.Id.ToString()),
            new(ClaimTypes.Name, userDto.NickName),
            new(ClaimTypes.Role, userDto.Role.ToString()),
            new("isBlocked", account.IsBlocked.ToString())
        };

        var secretKey = _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(secretKey)) throw new AuthenticationFailedException();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddDays(1),
            notBefore: DateTime.UtcNow,
            signingCredentials: creds
        );
        return token;
    }
}