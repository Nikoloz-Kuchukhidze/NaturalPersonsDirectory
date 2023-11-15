using FluentValidation;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed class NaturalPersonRelationCommandValidator : AbstractValidator<NaturalPersonRelationCommand>
{
    public NaturalPersonRelationCommandValidator()
    {
        // TODO: Define enum values
        RuleFor(x => x.RelationType);
    }
}
