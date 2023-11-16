using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Resources;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;

public sealed class ImageValidator : AbstractValidator<IFormFile>
{
    private readonly IConfiguration _configuration;

    public ImageValidator(
        IConfiguration configuration,
        IStringLocalizer<ValidationMessages> localizer)
    {
        _configuration = configuration;

        RuleFor(file => file)
            .Must(BeAllowedContentType)
            .WithMessage(localizer[ValidationMessageKey.ImageContentType]);
    }

    private bool BeAllowedContentType(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return false;
        }
        
        var allowedContentTypes = _configuration.GetSection("AllowedFileContentTypes").Get<string[]>();

        return allowedContentTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase);
    }
}