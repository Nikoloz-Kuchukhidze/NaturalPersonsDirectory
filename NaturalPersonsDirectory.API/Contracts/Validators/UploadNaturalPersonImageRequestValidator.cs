using FluentValidation;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;

namespace NaturalPersonsDirectory.API.Contracts.Validators;

public class UploadNaturalPersonImageRequestValidator : AbstractValidator<UploadNaturalPersonImageRequest>
{
    public UploadNaturalPersonImageRequestValidator(IConfiguration configuration)
    {
        RuleFor(x => x.Image)
            .SetValidator(new ImageValidator(configuration));
    }
}
