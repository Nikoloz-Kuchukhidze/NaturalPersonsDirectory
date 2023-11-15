using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.API.Contracts.NaturalPersons;

public sealed record AddRelationRequest(long RelatedNaturalPersonId, RelationType RelationType);