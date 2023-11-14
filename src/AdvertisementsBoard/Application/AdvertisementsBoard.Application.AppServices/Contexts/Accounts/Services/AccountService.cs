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
    /// <param name="userService"></param>
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
        var account = await _accountRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AccountAdminDto>(account);
        return dto;
    }

    /// <inheritdoc />
    public async Task<AccountInfoDto> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.UserId == userId, cancellationToken);

        var dto = _mapper.Map<AccountInfoDto>(account);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AccountShortInfoDto>> GetAllAsync(int pageSize, int pageNumber, bool? isBlocked,
        CancellationToken cancellationToken)
    {
        var listAccounts = await _accountRepository.GetAllAsync(cancellationToken, pageNumber, pageSize, isBlocked);

        var accountDtos = _mapper.Map<List<AccountShortInfoDto>>(listAccounts);
        return accountDtos;
    }

    /// <inheritdoc />
    public async Task<AccountCreatedDto> SignUpAsync(AccountCreateDto dto, CancellationToken cancellationToken)
    {
        _passwordService.ComparePasswords(dto.Password, dto.ConfirmPassword);

        var exist = await _accountRepository.DoesAccountExistWhereAsync(
            a => a.Email == dto.Email || a.User.NickName == dto.User.NickName, cancellationToken);

        if (exist) throw new AccountAlreadyExistsException();

        var account = _mapper.Map<Account>(dto);

        account.Created = DateTime.UtcNow;
        account.PasswordHash = _passwordService.HashPassword(dto.Password);

        var createdAccount = await _accountRepository.CreateAsync(account, cancellationToken);

        var createdDto = _mapper.Map<AccountCreatedDto>(createdAccount);
        return createdDto;
    }

    /// <inheritdoc />
    public async Task<string> SignInAsync(AccountSignInDto signInDto, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.Email == signInDto.Email, cancellationToken);
        var user = await _userService.GetByIdAsync(account.UserId, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(account.PasswordHash, signInDto.Password);

        var accountDto = _mapper.Map<AccountDto>(account);
        var userDto = _mapper.Map<UserSignInDto>(user);

        var token = JwtSecurityToken(accountDto, userDto);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    /// <inheritdoc />
    public async Task ChangePasswordAsync(Guid userId, AccountPasswordEditDto accountDto,
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.UserId == userId, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(account.PasswordHash, accountDto.CurrentPassword);

        _passwordService.ComparePasswords(accountDto.NewPassword, accountDto.ConfirmPassword);

        account.PasswordHash = _passwordService.HashPassword(accountDto.NewPassword);

        var updatedAccount = _mapper.Map(accountDto, account);

        await _accountRepository.UpdateAsync(updatedAccount, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AccountBlockStatusDto> BlockByIdAsync(Guid id, AccountBlockStatusDto dto,
        CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(id, cancellationToken);

        var updatedAccount = _mapper.Map<Account>(account);

        await _accountRepository.UpdateAsync(updatedAccount, cancellationToken);
        return dto;
    }

    /// <inheritdoc />
    public async Task<bool> IsAccountBlocked(Guid userId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(userId, cancellationToken);
        return account.IsBlocked;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private JwtSecurityToken JwtSecurityToken(AccountDto accountDto, UserSignInDto user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.NickName),
            new(ClaimTypes.Role, user.Role.ToString()),
            new("isBlocked", accountDto.IsBlocked.ToString())
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