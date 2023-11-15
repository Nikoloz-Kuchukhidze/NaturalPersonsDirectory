using FluentValidation;
using FluentValidation.AspNetCore;
using Mapster;
using NaturalPersonsDirectory.API.Common.Converters;
using NaturalPersonsDirectory.API.Middlewares;
using System.Reflection;
using System.Text.Json.Serialization;

namespace NaturalPersonsDirectory.API;

public static class DependencyInjection
{
    public static IServiceCollection AddAPI(this IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonConverters();

        services
            .ConfigureRouteOptions()
            .AddSwagger()
            .AddExceptionHandling()
            .AddFluentValidation()
            .AddMappings();

        return services;
    }

    private static IMvcBuilder AddJsonConverters(this IMvcBuilder builder)
    {
        return builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(config =>
        {
            config.DisableDataAnnotationsValidation = true;
        });

        var assemblies = new List<Assembly>
        {
            typeof(DependencyInjection).Assembly,
            typeof(Application.DependencyInjection).Assembly
        };

        return services.AddValidatorsFromAssemblies(assemblies);
    }

    private static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    private static IServiceCollection AddExceptionHandling(this IServiceCollection services)
    {
        return services.AddTransient<ExceptionHandlingMiddleware>();
    }

    private static IServiceCollection ConfigureRouteOptions(this IServiceCollection services)
    {
        return services.Configure<RouteOptions>(options =>
        {
            options.LowercaseUrls = true;
        });
    }

    private static IServiceCollection AddMappings(this IServiceCollection services)
    {
        var currentAssembly = typeof(DependencyInjection).Assembly;
        var applicationAssembly = typeof(Application.DependencyInjection).Assembly;

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(currentAssembly, applicationAssembly);
        services.AddSingleton(config);

        return services;
    }
}
