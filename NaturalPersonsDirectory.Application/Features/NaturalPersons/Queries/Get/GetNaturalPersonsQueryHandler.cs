using MediatR;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.Paging;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.Get;

internal sealed class GetNaturalPersonsQueryHandler : IRequestHandler<GetNaturalPersonsQuery, IPagedList<NaturalPersonResponse>>
{
    private readonly INaturalPersonRepository _naturalPersonRepository;

    public GetNaturalPersonsQueryHandler(INaturalPersonRepository naturalPersonRepository)
    {
        _naturalPersonRepository = naturalPersonRepository;
    }

    public async Task<IPagedList<NaturalPersonResponse>> Handle(GetNaturalPersonsQuery request, CancellationToken cancellationToken)
    {
        // TODO: if null or white space don't pass in expression
        var naturalPersons = await _naturalPersonRepository.GetAsync(
            x => x.FirstName.Contains(request.FirstName ?? string.Empty) &&
                x.LastName.Contains(request.LastName ?? string.Empty) &&
                x.PersonalNumber.Contains(request.PersonalNumber ?? string.Empty),
            request.Page,
            request.PageSize,
            cancellationToken: cancellationToken);

        return naturalPersons.Adapt<NaturalPersonResponse>();
    }
}
