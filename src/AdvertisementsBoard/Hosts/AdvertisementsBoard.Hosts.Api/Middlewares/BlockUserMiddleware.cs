using System.Security.Claims;
using AdvertisementsBoard.Application.AppServices.Contexts.Accounts.Services;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AuthenticationErrorExceptions;

namespace AdvertisementsBoard.Hosts.Api.Middlewares;

/// <summary>
/// </summary>
public class BlockUserMiddleware
{
    private readonly RequestDelegate _next;

    /// <summary>
    /// </summary>
    /// <param name="next"></param>
    public BlockUserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    /// <param name="accountService"></param>
    public async Task InvokeAsync(HttpContext context, IAccountService accountService)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        if (token != null)
        {
            var userIdValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdValue, out var userId)) throw new AuthenticationFailedException();
            var isBlocked = await accountService.IsAccountBlocked(userId, context.RequestAborted);
            if (isBlocked) throw new AccountForbiddenException();
        }

        await _next(context);
    }
}