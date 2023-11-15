using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.API.Contracts.Phones;

public sealed record CreatePhoneRequest(
    string Number,
    PhoneType Type);