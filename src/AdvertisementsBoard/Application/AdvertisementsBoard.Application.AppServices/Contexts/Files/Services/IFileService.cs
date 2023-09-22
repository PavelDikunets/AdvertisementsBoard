using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Files.Services;

/// <summary>
///     Сервис для работы с файлами.
/// </summary>
public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
}