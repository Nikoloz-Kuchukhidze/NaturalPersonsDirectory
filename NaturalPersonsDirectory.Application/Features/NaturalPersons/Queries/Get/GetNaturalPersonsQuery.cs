using MediatR;
using NaturalPersonsDirectory.Application.Common.Paging;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Domain.Common.Paging;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.Get;

public sealed record GetNaturalPersonsQuery(
    int? Page,
    int? PageSize,
    string? FirstName,
    string? LastName,
    string? PersonalNumber) : PagingRequest(Page, PageSize), IRequest<IPagedList<NaturalPersonResponse>>;