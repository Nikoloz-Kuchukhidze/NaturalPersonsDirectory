using MediatR;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;

namespace NaturalPersonsDirectory.Application.Features.Cities.Queries.GetById;

public sealed record GetCityByIdQuery(int Id) : IRequest<CityResponse>;