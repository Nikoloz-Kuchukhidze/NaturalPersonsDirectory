using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Common.Utils.Validators;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;
using NaturalPersonsDirectory.Domain.Common.Utils;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Create;

public sealed class CreateNaturalPersonCommandValidator : AbstractValidator<CreateNaturalPersonCommand>
{
    public CreateNaturalPersonCommandValidator(
        IDateTimeProvider dateTimeProvider,
        IConfiguration configuration,
        IStringLocalizer<ValidationMessages> localizer)
    {
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(TextValidator.ContainOnlyGeorgianOrEnglishLetters)
            .WithMessage(localizer[ValidationMessageKey.FirstNameGeorgianOrEnglish]);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(TextValidator.ContainOnlyGeorgianOrEnglishLetters)
            .WithMessage(localizer[ValidationMessageKey.LastNameGeorgianOrEnglish]);

        RuleFor(x => x.Gender)
            .IsInEnum();

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .Length(11)
            .Must(TextValidator.ContainOnlyNumbers)
            .WithMessage(localizer[ValidationMessageKey.PersonalNumberOnlyNumbers]);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(BirthDate => AgeValidator.BeOlderThanEighteen(BirthDate, dateTimeProvider.Now))
            .WithMessage(localizer[ValidationMessageKey.Age]);

        RuleFor(x => x.Image)
            .NotEmpty()
            .SetValidator(new ImageRequestValidator(configuration, localizer));

        RuleFor(x => x.Phones)
            .NotEmpty();

        RuleForEach(x => x.Phones)
            .SetValidator(new CreatePhoneCommandValidator());
    }
}
