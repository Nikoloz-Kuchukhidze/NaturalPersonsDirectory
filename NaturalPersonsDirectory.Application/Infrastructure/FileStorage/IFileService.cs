using Microsoft.AspNetCore.Http;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

namespace NaturalPersonsDirectory.Application.Infrastructure.FileStorage;

public interface IFileService
{
    Task<string> GetFilePublicUrl(string path);
    Task<string> UploadFile(IFormFile file, string fileName);
    Task<string> UploadFile(ImageRequest image, string fileName);
    Task RemoveFile(string path);
    Task<string> ReplaceFile(string path, IFormFile file);
}