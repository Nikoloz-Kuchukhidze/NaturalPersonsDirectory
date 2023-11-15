using MediatR;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;

namespace NaturalPersonsDirectory.Application.Features.Cities.Commands.Create;

public sealed record CreateCityCommand(string Name) : IRequest<CityResponse>;