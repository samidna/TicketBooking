using Microsoft.AspNetCore.Http;

namespace TicketBooking.Core.Interfaces;
public interface IFileService
{
    Task<string> UploadFileAsync(IFormFile file, string folderName);
    void DeleteFile(string filePath);
}
