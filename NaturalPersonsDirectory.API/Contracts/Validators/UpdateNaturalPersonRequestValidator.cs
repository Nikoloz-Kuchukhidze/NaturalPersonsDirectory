using FluentValidation;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Common.Utils.Validators;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Domain.Common.Utils;

namespace NaturalPersonsDirectory.API.Contracts.Validators;

public class UpdateNaturalPersonRequestValidator : AbstractValidator<UpdateNaturalPersonRequest>
{
    public UpdateNaturalPersonRequestValidator(
        IStringLocalizer<ValidationMessages> localizer,
        IDateTimeProvider dateTimeProvider)
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(TextValidator.ContainOnlyGeorgianOrEnglishLetters)
            .WithMessage(localizer[ValidationMessageKey.FirstNameGeorgianOrEnglish])
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(TextValidator.ContainOnlyGeorgianOrEnglishLetters)
            .WithMessage(localizer[ValidationMessageKey.LastNameGeorgianOrEnglish])
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));

        RuleFor(x => x.PersonalNumber)
            .Length(11)
            .Must(TextValidator.ContainOnlyNumbers)
            .WithMessage(localizer[ValidationMessageKey.PersonalNumberOnlyNumbers])
            .When(x => !string.IsNullOrWhiteSpace(x.PersonalNumber));

        RuleFor(x => x.BirthDate)
            .Must(birthdate => AgeValidator.BeOlderThanEighteen(birthdate!.Value, dateTimeProvider.Now))
            .WithMessage(localizer[ValidationMessageKey.Age])
            .When(x => x.BirthDate.HasValue);

        RuleFor(x => x.Gender)
            .IsInEnum()
            .When(x => x.Gender.HasValue);

        RuleForEach(x => x.Phones)
            .SetValidator(new CreatePhoneCommandValidator())
            .When(x => x.Phones != null && x.Phones.Any());
    }
}
