using Microsoft.AspNetCore.Http;

namespace NaturalPersonsDirectory.Application.Infrastructure.FileStorage;

public interface IFileService
{
    Task<string> GetFilePublicUrl(string path);
    Task<string> UploadFile(IFormFile file, string fileName);
    Task RemoveFile(string path);
    Task<string> ReplaceFile(string path, IFormFile file);
}