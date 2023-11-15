using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Update;

internal sealed class UpdateNaturalPersonCommandHandler : IRequestHandler<UpdateNaturalPersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public UpdateNaturalPersonCommandHandler(
        IUnitOfWork unitOfWork,
        INaturalPersonRepository naturalPersonRepository,
        ICityRepository cityRepository,
        IPhoneRepository phoneRepository,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _unitOfWork = unitOfWork;
        _naturalPersonRepository = naturalPersonRepository;
        _cityRepository = cityRepository;
        _phoneRepository = phoneRepository;
        _localizer = localizer;
    }

    public async Task Handle(UpdateNaturalPersonCommand request, CancellationToken cancellationToken)
    {
        var naturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id && x.IsActive,
            asNoTracking: false,
            cancellationToken,
            includes: x => x.Phones);

        if (naturalPerson is null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.Id),
                request.Id.ToString(),
                _localizer);
        }

        if (request.CityId.HasValue)
        {
            var cityExists = await _cityRepository.ExistAsync(
                x => x.Id == request.CityId,
                cancellationToken: cancellationToken);

            if (!cityExists)
            {
                throw new NotFoundException(
                    nameof(City),
                    nameof(request.CityId),
                    request.CityId.Value.ToString(),
                    _localizer);
            }

            naturalPerson.UpdateCity(request.CityId.Value);
        }

        if (!string.IsNullOrWhiteSpace(request.PersonalNumber) && naturalPerson.PersonalNumber != request.PersonalNumber)
        {
            var personalNumberExists = await _naturalPersonRepository.ExistAsync(
                x => x.PersonalNumber == request.PersonalNumber && x.IsActive,
                cancellationToken);

            if (personalNumberExists)
            {
                throw new AlreadyExistsException(
                    nameof(NaturalPerson),
                    nameof(request.PersonalNumber),
                    request.PersonalNumber,
                    _localizer);
            }

            naturalPerson.UpdatePersonalNumber(request.PersonalNumber);
        }

        await UpdateIfValidPhones(naturalPerson, request.Phones, cancellationToken);

        naturalPerson.Update(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.BirthDate);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateIfValidPhones(
        NaturalPerson naturalPerson,
        IEnumerable<CreatePhoneCommand>? phones,
        CancellationToken cancellationToken)
    {
        // TODO: if existing ids do not belong to anyone we will add them to natural person
        if (phones != null && phones.Any())
        {
            var phoneBelongsToSomeoneElse = await _phoneRepository.ExistAsync(
                x => phones.Select(y => y.Number).Contains(x.Number)
                    && phones.Select(y => y.Id).Contains(x.Id)
                    && naturalPerson.Id != x.NaturalPersonId,
                cancellationToken);

            if (phoneBelongsToSomeoneElse)
            {
                throw new PhoneBelongsToSomeoneElseException(_localizer);
            }

            var newPhones = phones.Adapt<IEnumerable<Phone>>();

            naturalPerson.UpdatePhones(newPhones);
        }
    }
}