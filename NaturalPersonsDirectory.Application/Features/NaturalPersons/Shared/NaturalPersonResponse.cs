using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;

public sealed record NaturalPersonResponse(
    long Id,
    string FirstName,
    string LastName,
    Gender Gender,
    string PersonalNumber,
    DateOnly BirthDate,
    DateTimeOffset CreatedOn,
    DateTimeOffset? UpdatedOn,
    int? CityId,
    string City,
    IEnumerable<PhoneResponse> Phones,
    IEnumerable<NaturalPersonRelationResponse> Relations)
{
    public string Image { get; set; } = null!;
}

public sealed record NaturalPersonRelationResponse(
    NaturalPersonResponse RelatedNaturalPerson,
    RelationType RelationType);

public sealed record PhoneResponse(
    long Id,
    string Number,
    PhoneType Type);