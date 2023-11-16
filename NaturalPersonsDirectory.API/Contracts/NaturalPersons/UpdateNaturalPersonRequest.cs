using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.API.Contracts.NaturalPersons;

public sealed record UpdateNaturalPersonRequest(
    string FirstName,
    string? LastName,
    Gender? Gender,
    string? PersonalNumber,
    DateOnly? BirthDate,
    int? CityId,
    IEnumerable<CreatePhoneCommand>? Phones);