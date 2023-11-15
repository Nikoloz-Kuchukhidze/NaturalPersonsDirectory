using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;

public sealed record NaturalPersonWithRelationsCountResponse(
    long Id,
    Dictionary<RelationType, int> RelationsCount);