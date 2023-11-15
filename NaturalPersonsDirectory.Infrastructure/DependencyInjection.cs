using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.Paging;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Infrastructure.FileStorage.Options;
using NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.UOW;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;
using NaturalPersonsDirectory.Infrastructure.Persistence.Options;
using NaturalPersonsDirectory.Infrastructure.Persistence.Repositories;
using Supabase;

namespace NaturalPersonsDirectory.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext(configuration)
            .AddRepositories()
            .AddUnitOfWork()
            .AddPaging()
            .AddFileStorage();
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        services.ConfigureOptions<DatabaseOptionsSetup>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var databaseOptions = serviceProvider.GetService<IOptions<DatabaseOptions>>()!.Value;

            options
                .UseSqlServer(databaseOptions.ConnectionString)
                .EnableDetailedErrors(databaseOptions.EnableDetailedErrors)
                .EnableSensitiveDataLogging(databaseOptions.EnableSensitiveDataLogging);
        });

        return services;
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddScoped<INaturalPersonRepository, NaturalPersonRepository>()
            .AddScoped<ICityRepository, CityRepository>()
            .AddScoped<IPhoneRepository, PhoneRepository>()
            .AddScoped<INaturalPersonRelationRepository, NaturalPersonRelationRepository>();
    }

    private static IServiceCollection AddUnitOfWork(this IServiceCollection services)
    {
        return services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static IServiceCollection AddPaging(this IServiceCollection services)
    {
        return services.AddTransient(typeof(IPagedList<>), typeof(PagedList<>));
    }

    private static IServiceCollection AddFileStorage(this IServiceCollection services)
    {
        services.ConfigureOptions<FileStorageOptionsSetup>();

        services.AddScoped(serviceProvider =>
        {
            var fileStorageOptions = serviceProvider.GetService<IOptions<FileStorageOptions>>()!.Value;

            var client = new Supabase.Client(
                fileStorageOptions.FileStorageUrl,
                fileStorageOptions.FileStorageKey,
                new SupabaseOptions
                {
                    AutoRefreshToken = true,
                    AutoConnectRealtime = true
                });

            return client;
        });

        services.AddScoped<IFileService, FileService>();

        return services;
    }
}
