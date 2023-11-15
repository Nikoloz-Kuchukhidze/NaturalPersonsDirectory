using FluentValidation;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed class NaturalPersonRelationCommandValidator : AbstractValidator<NaturalPersonRelationCommand>
{
    public NaturalPersonRelationCommandValidator()
    {
        RuleFor(x => x.RelationType)
            .IsInEnum();
    }
}
