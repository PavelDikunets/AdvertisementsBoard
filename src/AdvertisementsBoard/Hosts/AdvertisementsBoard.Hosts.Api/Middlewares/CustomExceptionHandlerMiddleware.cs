using System.Text.Json;
using AdvertisementsBoard.Common.ErrorExceptions.AccountErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AdvertisementErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.AttachmentErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.CategoryErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.PasswordErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.SubCategoryErrorExceptions;
using AdvertisementsBoard.Common.ErrorExceptions.UserErrorExceptions;

namespace AdvertisementsBoard.Hosts.Api.Middlewares;

/// <summary>
/// </summary>
public class CustomExceptionHandlerMiddleware
{
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    /// <summary>
    /// </summary>
    /// <param name="next"></param>
    /// <param name="logger"></param>
    public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// </summary>
    /// <param name="context"></param>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex, _logger);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex, ILogger logger)
    {
        context.Response.ContentType = "application/json";
        var result = JsonSerializer.Serialize(new { message = ex.Message });

        switch (ex)
        {
            default:
                context.Response.StatusCode = 500;
                logger.LogError(ex, "{Message}", ex.Message);
                break;

            case CategoryAlreadyExistsException or SubCategoryAlreadyExistsException or AccountAlreadyExistsException:
                context.Response.StatusCode = 409;
                logger.LogInformation(ex, "{Message}", ex.Message);
                break;

            case AdvertisementNotFoundException or AttachmentNotFoundException or CategoryNotFoundException
                or SubCategoryNotFoundException or UserNotFoundException or AccountNotFoundException:
                context.Response.StatusCode = 404;
                logger.LogInformation(ex, "{Message}", ex.Message);
                break;

            case AdvertisementForbiddenException or AccountForbiddenException:
                context.Response.StatusCode = 403;
                logger.LogInformation(ex, "{Message}", ex.Message);
                break;

            case InvalidSignInCredentialsException:
                context.Response.StatusCode = 401;
                logger.LogInformation(ex, "{Message}", ex.Message);
                break;

            case PasswordMismatchException or InvalidUserIdException:
                context.Response.StatusCode = 400;
                logger.LogInformation(ex, "{Message}", ex.Message);
                break;
        }

        return context.Response.WriteAsync(result);
    }
}