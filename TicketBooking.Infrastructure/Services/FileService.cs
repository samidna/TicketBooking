using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using TicketBooking.Core.Interfaces;

namespace TicketBooking.Infrastructure.Services;
public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> UploadFileAsync(IFormFile file, string folderName)
    {
        if (file == null || file.Length == 0)
            throw new Exception("File is empty or null.");

        string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", folderName);

        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        string uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return Path.Combine("uploads", folderName, uniqueFileName).Replace("\\", "/");
    }

    public void DeleteFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath)) return;

        string fullPath = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

        if (File.Exists(fullPath))
        {
            File.Delete(fullPath);
        }
    }
}
