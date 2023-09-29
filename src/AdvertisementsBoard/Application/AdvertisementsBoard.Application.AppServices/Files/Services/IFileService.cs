using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Application.AppServices.Files.Services;

/// <summary>
///     Сервис для работы с файлами.
/// </summary>
public interface IFileService
{
    /// <summary>
    ///     Загрузить файл на сервер.
    /// </summary>
    /// <param name="file">Файл.</param>
    /// <param name="cancellationToken">Токен отмены операции.</param>
    /// <returns>Относительный путь к файлу.</returns>
    Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken);
}