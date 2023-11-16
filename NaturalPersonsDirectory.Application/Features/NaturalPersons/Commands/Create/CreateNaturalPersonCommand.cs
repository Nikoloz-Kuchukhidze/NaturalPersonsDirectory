using MediatR;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Create;

public sealed record CreateNaturalPersonCommand(
    string FirstName,
    string LastName,
    Gender Gender,
    string PersonalNumber,
    DateOnly BirthDate,
    ImageRequest Image,
    int CityId,
    IEnumerable<NaturalPersonRelationCommand>? Relations,
    IEnumerable<CreatePhoneCommand> Phones) : IRequest<NaturalPersonResponse>;
