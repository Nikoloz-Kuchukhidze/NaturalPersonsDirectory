using Mapster;
using MediatR;
using NaturalPersonsDirectory.Application.Features.Cities.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.Cities.Commands.Create;

internal sealed class CreateCityCommandHandler : IRequestHandler<CreateCityCommand, CityResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ICityRepository _cityRepository;

    public CreateCityCommandHandler(IUnitOfWork unitOfWork, ICityRepository cityRepository)
    {
        _unitOfWork = unitOfWork;
        _cityRepository = cityRepository;
    }

    public async Task<CityResponse> Handle(CreateCityCommand request, CancellationToken cancellationToken)
    {
        var city = City.Create(request.Name);

        await _cityRepository.AddAsync(city, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return city.Adapt<CityResponse>();
    }
}
