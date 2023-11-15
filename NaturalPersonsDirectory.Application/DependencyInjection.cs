using Mapster;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Utils;
using NaturalPersonsDirectory.Domain.Common.Constants;
using NaturalPersonsDirectory.Domain.Common.Utils;
using System.Globalization;

namespace NaturalPersonsDirectory.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        return services
            .AddMediator()
            .AddDateTimeProvider()
            .AddCustomLocalization();
    }

    private static IServiceCollection AddMediator(this IServiceCollection services)
    {
        return services.AddMediatR(config => 
            config.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
    }

    private static IServiceCollection AddDateTimeProvider(this IServiceCollection services)
    {
        return services.AddSingleton<IDateTimeProvider>(_ =>
        {
            return new DateTimeProvider(TimeZoneId.GE);
        });
    }

    private static IServiceCollection AddCustomLocalization(this IServiceCollection services)
    {
        services.AddLocalization();

        services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo(CultureName.EnglishUnitedStates),
                new CultureInfo(CultureName.GeorgianGeorgia)
            };

            options.DefaultRequestCulture = new RequestCulture(
                culture: CultureName.EnglishUnitedStates,
                uiCulture: CultureName.EnglishUnitedStates);

            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
        });

        return services;
    }
}