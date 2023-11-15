using MediatR;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.Paging;

namespace NaturalPersonsDirectory.Application.Features.Cities.Queries.Get;

internal sealed class GetCitiesQueryHandler : IRequestHandler<GetCitiesQuery, IPagedList<CityResponse>>
{
    private readonly ICityRepository _cityRepository;

    public GetCitiesQueryHandler(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<IPagedList<CityResponse>> Handle(GetCitiesQuery request, CancellationToken cancellationToken)
    {
        var cities = await _cityRepository.GetAsync(
            _ => true,
            request.Page,
            request.PageSize,
            cancellationToken: cancellationToken);

        return cities.Adapt<CityResponse>();
    }
}
