using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed record NaturalPersonRelationCommand(
    long RelatedNaturalPersonId,
    RelationType RelationType);