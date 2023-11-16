using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed record CreatePhoneCommand(
    string Number,
    PhoneType Type);