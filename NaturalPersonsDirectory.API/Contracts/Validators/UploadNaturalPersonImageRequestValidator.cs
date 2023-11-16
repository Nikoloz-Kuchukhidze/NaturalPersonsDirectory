using FluentValidation;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;

namespace NaturalPersonsDirectory.API.Contracts.Validators;

public class UploadNaturalPersonImageRequestValidator : AbstractValidator<UploadNaturalPersonImageRequest>
{
    public UploadNaturalPersonImageRequestValidator(
        IConfiguration configuration,
        IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.Image)
            .SetValidator(new ImageValidator(configuration, localizer));
    }
}
