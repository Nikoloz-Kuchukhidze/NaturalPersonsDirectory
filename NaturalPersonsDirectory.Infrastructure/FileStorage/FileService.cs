using Microsoft.AspNetCore.Http;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using Supabase.Storage;

namespace NaturalPersonsDirectory.Infrastructure.FileStorage;

internal sealed class FileService : IFileService
{
    private readonly string _bucketName = "images";

    private readonly Supabase.Client _client;

    public FileService(Supabase.Client client)
    {
        _client = client;
    }

    public async Task<string> UploadFile(IFormFile file, string fileName)
    {
        await CreateBucketIfNotExists();

        var bytes = await ReadAllBytes(file);

        var extension = GetFileExtension(file);

        var path = $"{fileName}.{extension}";

        await _client.Storage
            .From(_bucketName)
            .Upload(bytes, path);

        return path;
    }

    public async Task<string> GetFilePublicUrl(string path)
    {
        await CreateBucketIfNotExists();

        var filePublicUrl = _client.Storage
            .From(_bucketName)
            .GetPublicUrl(path);

        return filePublicUrl;
    }

    public async Task RemoveFile(string path)
    {
        await CreateBucketIfNotExists();

        await _client.Storage
            .From(_bucketName)
            .Remove(path);
    }

    public async Task<string> ReplaceFile(string path, IFormFile file)
    {
        await RemoveFile(path);

        var fileName = RemoveFileExtension(path);

        return await UploadFile(file, fileName);
    }

    private async Task<byte[]> ReadAllBytes(IFormFile file)
    {
        using var memoryStream = new MemoryStream();

        await file.CopyToAsync(memoryStream);

        return memoryStream.ToArray();
    }

    private string GetFileExtension(IFormFile file)
    {
        var lastIndexOfDot = file.FileName.LastIndexOf('.');
        return file.FileName.Substring(lastIndexOfDot + 1);
    }

    private string RemoveFileExtension(string path)
    {
        var lastIndexOfDot = path.LastIndexOf('.');
        return path.Substring(0, lastIndexOfDot);
    }

    private async Task CreateBucketIfNotExists()
    {
        if (!(await BucketExists()))
        {
            await CreateBucket();
        }
    }

    private async Task CreateBucket()
    {
        await _client.Storage.CreateBucket(
                _bucketName,
                new BucketUpsertOptions
                {
                    Public = true
                });
    }

    private async Task<bool> BucketExists()
    {
        var buckets = await GetBuckets();

        return buckets != null && buckets.Any(x => x.Id == _bucketName);
    }

    private async Task<IEnumerable<Bucket>?> GetBuckets()
    {
        return await _client.Storage
            .ListBuckets();
    }
}