using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Application.AppServices.Contexts.Files.Services;

public class FileService : IFileService
{
    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        var uploadPath = Path.Combine("../../Infrastructure/AdvertisementsBoard.Infrastructure/FilesStorage");

        var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploadPath, filename);
        if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return filename;
    }
}