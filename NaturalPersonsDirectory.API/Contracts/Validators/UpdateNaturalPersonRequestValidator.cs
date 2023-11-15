using FluentValidation;
using NaturalPersonsDirectory.API.Contracts.NaturalPersons;
using NaturalPersonsDirectory.Application.Common.Constants;

namespace NaturalPersonsDirectory.API.Contracts.Validators;

public class UpdateNaturalPersonRequestValidator : AbstractValidator<UpdateNaturalPersonRequest>
{
    public UpdateNaturalPersonRequestValidator()
    {
        RuleFor(x => x.FirstName)
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(ContainOnlyGeorgianOrEnglishLetters)
            .When(x => !string.IsNullOrWhiteSpace(x.FirstName));

        RuleFor(x => x.LastName)
            .MinimumLength(2)
            .MaximumLength(50)
            .Must(ContainOnlyGeorgianOrEnglishLetters)
            .When(x => !string.IsNullOrWhiteSpace(x.LastName));
    }

    private bool ContainOnlyGeorgianOrEnglishLetters(string text)
    {
        var containsOnlyGeorgian = text.All(c => c >= UnicodeCharacter.GeorgianFirstLetter && c <= UnicodeCharacter.GeorgianLastLetter);

        var containsOnlyEnglish = text.All(c => (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z'));

        return containsOnlyGeorgian || containsOnlyEnglish;
    }
}
