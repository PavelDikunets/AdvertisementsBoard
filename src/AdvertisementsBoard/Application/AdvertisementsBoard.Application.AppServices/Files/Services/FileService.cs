using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace AdvertisementsBoard.Application.AppServices.Files.Services;

/// <inheritdoc />
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    /// <inheritdoc />
    public async Task<string> UploadFileAsync(IFormFile file, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_environment.WebRootPath))
            _environment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        var uploads = Path.Combine(_environment.WebRootPath, "uploads");
        var filename = Guid.NewGuid() + Path.GetExtension(file.FileName);
        var filePath = Path.Combine(uploads, filename);

        Directory.CreateDirectory(uploads);

        await using var stream = new FileStream(filePath, FileMode.Create);
        await file.CopyToAsync(stream, cancellationToken);

        return Path.Combine("uploads", filename).Replace("\\", "/");
    }
}