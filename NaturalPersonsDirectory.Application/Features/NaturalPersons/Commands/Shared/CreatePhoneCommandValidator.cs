using FluentValidation;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed class CreatePhoneCommandValidator : AbstractValidator<CreatePhoneCommand>
{
    public CreatePhoneCommandValidator()
    {
        RuleFor(x => x.Number)
            .MinimumLength(4)
            .MaximumLength(50);

        // TODO: Define only enum values
        RuleFor(x => x.Type);
    }
}
