using FluentValidation;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Constants;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared.Validators;
using NaturalPersonsDirectory.Domain.Common.Utils;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Create;

public sealed class CreateNaturalPersonCommandValidator : AbstractValidator<CreateNaturalPersonCommand>
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreateNaturalPersonCommandValidator(
        IDateTimeProvider dateTimeProvider,
        IConfiguration configuration,
        IStringLocalizer<ValidationMessages> _localizer)
    {
        _dateTimeProvider = dateTimeProvider;

        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(ContainOnlyGeorgianOrEnglishLetters).WithMessage(_localizer["FirstNameGeorgianOrEnglish"]);

        RuleFor(x => x.LastName)
            .NotEmpty()
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(ContainOnlyGeorgianOrEnglishLetters);

        /* TEST: which one? 
         RuleFor(x => x.Gender).IsEnumName(typeof(Gender));*/
        RuleFor(x => x.Gender)
            .Must(value => Enum.IsDefined(typeof(Gender), value));

        RuleFor(x => x.PersonalNumber)
            .NotEmpty()
            .Length(11)
            .Must(ContainOnlyNumbers);

        RuleFor(x => x.BirthDate)
            .NotEmpty()
            .Must(BeOlderThanEighteen);

        //RuleFor(x => x.Image)
        //    .SetValidator(new ImageValidator(configuration));
    }

    private bool ContainOnlyGeorgianOrEnglishLetters(string text)
    {
        var containsOnlyGeorgian = text.All(c => c >= UnicodeCharacter.GeorgianFirstLetter && c <= UnicodeCharacter.GeorgianLastLetter);

        var containsOnlyEnglish = text.All(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));

        return containsOnlyGeorgian || containsOnlyEnglish;
    }

    private bool ContainOnlyNumbers(string text)
    {
        return text.All(c => char.IsNumber(c));
    }

    private bool BeOlderThanEighteen(DateOnly birthDate)
    {
        var currentDate = _dateTimeProvider.Now;

        return currentDate.Year - birthDate.Year >= 18 ||
            currentDate.Year - birthDate.Year >= 17 &&
            (currentDate.Month < birthDate.Month || (currentDate.Month == birthDate.Month && currentDate.Day < birthDate.Day));
    }
}
