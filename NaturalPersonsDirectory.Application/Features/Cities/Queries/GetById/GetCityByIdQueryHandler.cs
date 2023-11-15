using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.Cities.Queries.GetById;

internal sealed class GetCityByIdQueryHandler : IRequestHandler<GetCityByIdQuery, CityResponse>
{
    private readonly ICityRepository _cityRepository;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public GetCityByIdQueryHandler(ICityRepository cityRepository, IStringLocalizer<ExceptionMessages> localizer)
    {
        _cityRepository = cityRepository;
        _localizer = localizer;
    }

    public async Task<CityResponse> Handle(GetCityByIdQuery request, CancellationToken cancellationToken)
    {
        var city = await _cityRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id,
            cancellationToken: cancellationToken);

        if (city is null)
        {
            throw new NotFoundException(
                nameof(City),
                nameof(request.Id),
                request.Id.ToString(),
                _localizer);
        }

        return new CityResponse(city.Id, city.Name);
    }
}
