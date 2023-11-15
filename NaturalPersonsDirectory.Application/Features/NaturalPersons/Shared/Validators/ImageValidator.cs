using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;

public sealed class ImageValidator : AbstractValidator<IFormFile>
{
    private readonly IConfiguration _configuration;

    public ImageValidator(IConfiguration configuration)
    {
        _configuration = configuration;

        RuleFor(file => file)
            .Must(BeValidImage);
    }

    private bool BeValidImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return false;
        }
        
        var allowedContentTypes = _configuration.GetSection("AllowedFileContentTypes").Get<string[]>();

        return allowedContentTypes.Contains(file.ContentType, StringComparer.OrdinalIgnoreCase);
    }
}