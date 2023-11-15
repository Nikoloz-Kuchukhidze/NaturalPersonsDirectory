using MediatR;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;

internal sealed class GetNaturalPersonsWithRelationsCountQueryHandler :
    IRequestHandler<GetNaturalPersonsWithRelationsCountQuery, IEnumerable<NaturalPersonWithRelationsCountResponse>>
{
    private readonly INaturalPersonRepository _repository;

    public GetNaturalPersonsWithRelationsCountQueryHandler(INaturalPersonRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<NaturalPersonWithRelationsCountResponse>> Handle(
        GetNaturalPersonsWithRelationsCountQuery request, 
        CancellationToken cancellationToken)
    {
        return await _repository.GetNaturalPersonsWithRelationsCount();
    }
}