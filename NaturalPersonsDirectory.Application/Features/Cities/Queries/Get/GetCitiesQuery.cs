using MediatR;
using NaturalPersonsDirectory.Application.Common.Paging;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;
using NaturalPersonsDirectory.Domain.Common.Paging;

namespace NaturalPersonsDirectory.Application.Features.Cities.Queries.Get;

public sealed record GetCitiesQuery(int? Page, int? PageSize) 
    : PagingRequest(Page, PageSize), IRequest<IPagedList<CityResponse>>;