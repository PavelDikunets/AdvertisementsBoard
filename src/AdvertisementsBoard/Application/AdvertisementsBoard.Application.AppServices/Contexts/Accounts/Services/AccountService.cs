using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Repositories;
using AdvertisementsBoard.Application.AppServices.Contexts.Users.Services;
using AdvertisementsBoard.Application.AppServices.Services.Passwords.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;
using AdvertisementsBoard.Contracts.Accounts;
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
    public async Task<AccountInfoDto> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.GetByIdAsync(id, cancellationToken);

        var dto = _mapper.Map<AccountInfoDto>(account);
        return dto;
    }

    /// <inheritdoc />
    public async Task<List<AccountShortInfoDto>> GetAllAsync(int pageSize, int pageNumber, bool? isBlocked,
        CancellationToken cancellationToken)
    {
        var listAccounts = await _accountRepository.GetAllAsync(cancellationToken, pageNumber, pageSize, isBlocked);
        return listAccounts;
    }

    /// <inheritdoc />
    public async Task<AccountCreatedDto> SignUpAsync(AccountSignUpDto dto, CancellationToken cancellationToken)
    {
        _passwordService.ComparePasswords(dto.Password, dto.ConfirmPassword);

        var exit = await _accountRepository.DoesAccountExistWhereAsync(
            a => a.Email == dto.Email || a.User.NickName == dto.User.NickName, cancellationToken);

        if (exit) throw new AccountAlreadyExistsException();

        var newAccount = _mapper.Map<AccountDto>(dto);

        newAccount.Created = DateTime.UtcNow;
        newAccount.PasswordHash = _passwordService.HashPassword(dto.Password);

        var createdAccount = await _accountRepository.CreateAsync(newAccount, cancellationToken);
        return createdAccount;
    }

    /// <inheritdoc />
    public async Task<string> SignInAsync(AccountSignInDto dto, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.Email == dto.Email, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(account.PasswordHash, dto.Password);

        var token = JwtSecurityToken(account);

        var jwt = new JwtSecurityTokenHandler().WriteToken(token);
        return jwt;
    }

    /// <inheritdoc />
    public async Task ChangePasswordAsync(Guid userId, AccountPasswordEditDto editDto,
        CancellationToken cancellationToken)
    {
        await _userService.DoesUserExistByIdAsync(userId, cancellationToken);

        var account = await _accountRepository.FindWhereAsync(a => a.User.Id == userId, cancellationToken);

        _passwordService.ComparePasswordHashWithPassword(account.PasswordHash, editDto.CurrentPassword);

        _passwordService.ComparePasswords(editDto.NewPassword, editDto.ConfirmPassword);

        _mapper.Map(editDto, account);

        account.PasswordHash = _passwordService.HashPassword(editDto.NewPassword);

        await _accountRepository.UpdateAsync(account, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<AccountBlockDto> BlockByIdAsync(Guid id, AccountBlockDto dto, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.Id == id, cancellationToken);

        _mapper.Map(dto, account);

        await _accountRepository.UpdateAsync(account, cancellationToken);
        return dto;
    }

    /// <inheritdoc />
    public async Task<bool> IsAccountBlocked(Guid userId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.User.Id == userId, cancellationToken);
        return account.IsBlocked;
    }

    public async Task<AccountInfoDto> GetCurrentByUserId(Guid userId, CancellationToken cancellationToken)
    {
        var account = await _accountRepository.FindWhereAsync(a => a.User.Id == userId, cancellationToken);

        var dto = _mapper.Map<AccountInfoDto>(account);
        return dto;
    }

    /// <inheritdoc />
    public async Task DeleteByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        await _accountRepository.DeleteByIdAsync(id, cancellationToken);
    }


    private JwtSecurityToken JwtSecurityToken(AccountDto account)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, account.User.Id.ToString()),
            new(ClaimTypes.Name, account.User.NickName),
            new(ClaimTypes.Role, account.User.Role.ToString()),
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