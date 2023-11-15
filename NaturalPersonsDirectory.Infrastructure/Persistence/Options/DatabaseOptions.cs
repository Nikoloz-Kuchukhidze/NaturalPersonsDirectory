namespace NaturalPersonsDirectory.Infrastructure.Persistence.Options;

public class DatabaseOptions
{
    public string ConnectionString { get; set; } = string.Empty;
    public bool EnableDetailedErrors { get; set; }
    public bool EnableSensitiveDataLogging { get; set; }
}
