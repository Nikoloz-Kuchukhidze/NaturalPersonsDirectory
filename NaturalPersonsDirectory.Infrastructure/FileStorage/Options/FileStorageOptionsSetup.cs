using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace NaturalPersonsDirectory.Infrastructure.FileStorage.Options;

public class FileStorageOptionsSetup : IConfigureOptions<FileStorageOptions>
{
    private readonly IConfiguration _configuration;

    public FileStorageOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(FileStorageOptions options)
    {
        _configuration.GetSection(nameof(FileStorageOptions))
            .Bind(options);
    }
}
