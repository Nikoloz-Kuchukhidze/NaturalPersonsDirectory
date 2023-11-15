using MediatR;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Delete;

public sealed record DeleteNaturalPersonCommand(long Id) : IRequest;