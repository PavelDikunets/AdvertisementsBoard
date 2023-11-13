using System.Security.Claims;

namespace AdvertisementsBoard.Hosts.Api.Extensions;

/// <summary>
/// </summary>
public static class HttpContextExtensions
{
    /// <summary>
    /// </summary>
    /// <param name="httpContext"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static Guid GetUserId(this HttpContext httpContext)
    {
        if (httpContext == null) throw new InvalidOperationException("HTTP-контекст не доступен.");

        var userIdValue = httpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!Guid.TryParse(userIdValue, out var userId))
            throw new ArgumentException("Невалидный идентификатор.");

        return userId;
    }
}