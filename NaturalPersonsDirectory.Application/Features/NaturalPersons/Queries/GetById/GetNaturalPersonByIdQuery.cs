using MediatR;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetById;

public sealed record GetNaturalPersonByIdQuery(long Id) : IRequest<NaturalPersonResponse>;