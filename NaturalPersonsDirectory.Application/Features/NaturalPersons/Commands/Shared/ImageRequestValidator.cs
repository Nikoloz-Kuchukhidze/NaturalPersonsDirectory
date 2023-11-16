using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Resources;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public class ImageRequestValidator : AbstractValidator<ImageRequest>
{
    private readonly IConfiguration _configuration;
    private readonly string[] _allowedFileExtensions = new[] { "png", "jpg", "jpeg", "jfif", "pjpeg", "pjp" };

    public ImageRequestValidator(IConfiguration configuration, IStringLocalizer<ValidationMessages> localizer)
    {
        _configuration = configuration;

        RuleFor(x => x.ContentType)
            .NotEmpty()
            .Must(BeAllowedContentType)
            .WithMessage(localizer[ValidationMessageKey.ImageContentType]);

        RuleFor(x => x.FileName)
            .NotEmpty()
            .Must(HaveAllowedExtension)
            .WithMessage(localizer[ValidationMessageKey.NotAllowedFileExtension]);

        RuleFor(x => x.Data)
            .NotEmpty();
    }

    private bool BeAllowedContentType(string contentType)
    {
        var allowedContentTypes = _configuration.GetSection("AllowedFileContentTypes").Get<string[]>();

        return allowedContentTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase);
    }

    private bool HaveAllowedExtension(string fileName)
    {
        var lastIndexOfDot = fileName.LastIndexOf('.');
        var fileExtension = fileName.Substring(lastIndexOfDot + 1);

        return _allowedFileExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
    }
}
