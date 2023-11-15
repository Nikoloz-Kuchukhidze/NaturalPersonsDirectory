using NaturalPersonsDirectory.API.Contracts.Phones;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.API.Contracts.NaturalPersons;

public sealed record UpdateNaturalPersonRequest(
    string FirstName,
    string? LastName,
    Gender? Gender,
    string? PersonalNumber,
    DateOnly? BirthDate,
    int? CityId,
    IEnumerable<CreatePhoneRequest>? Phones);