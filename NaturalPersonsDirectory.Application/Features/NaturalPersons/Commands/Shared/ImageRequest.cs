namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;

public sealed record ImageRequest(
    string FileName,
    string ContentType,
    byte[] Data);
