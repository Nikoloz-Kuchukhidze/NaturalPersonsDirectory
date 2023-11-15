using MediatR;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;

public sealed record GetNaturalPersonsWithRelationsCountQuery() 
    : IRequest<IEnumerable<NaturalPersonWithRelationsCountResponse>>;