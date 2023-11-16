using MediatR;
using NaturalPersonsDirectory.Application.Common.Extensions;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.Paging;
using NaturalPersonsDirectory.Domain.Entities;
using System.Linq.Expressions;

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
        var predicates = new List<Expression<Func<NaturalPerson, bool>>>();

        if (!string.IsNullOrWhiteSpace(request.FirstName))
        {
            predicates.Add(x => x.FirstName.Contains(request.FirstName));
        }

        if (!string.IsNullOrWhiteSpace(request.LastName))
        {
            predicates.Add(x => x.LastName.Contains(request.LastName));
        }

        if (!string.IsNullOrEmpty(request.PersonalNumber))
        {
            predicates.Add(x => x.PersonalNumber.Contains(request.PersonalNumber));
        }

        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            predicates.Add(x =>
                (x.PersonalNumber.Contains(request.SearchTerm) ||
                x.FirstName.Contains(request.SearchTerm) ||
                x.LastName.Contains(request.SearchTerm) ||
                x.City!.Name.Contains(request.SearchTerm)));
        }

        var predicate = predicates.Aggregate((current, next) => current.And(next));

        var naturalPersons = await _naturalPersonRepository.GetWithPagingAsync(
            predicate,
            request.Page,
            request.PageSize,
            cancellationToken: cancellationToken);

        return naturalPersons.Adapt<NaturalPersonResponse>();
    }
}