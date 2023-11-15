using MediatR;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Update;

public sealed record UpdateNaturalPersonCommand(
    string? FirstName,
    string? LastName,
    Gender? Gender,
    string? PersonalNumber,
    DateOnly? BirthDate,
    int? CityId,
    IEnumerable<CreatePhoneCommand>? Phones) : IRequest
{
    public long Id { get; set; }
}