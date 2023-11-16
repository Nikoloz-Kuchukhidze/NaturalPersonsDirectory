using FluentValidation;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed class CreatePhoneCommandValidator : AbstractValidator<CreatePhoneCommand>
{
    public CreatePhoneCommandValidator()
    {
        RuleFor(x => x.Number)
            .NotEmpty()
            .MinimumLength(4)
            .MaximumLength(50);

        RuleFor(x => x.Type)
            .IsInEnum();
    }
}
