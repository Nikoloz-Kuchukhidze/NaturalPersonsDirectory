using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Shared;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage.Constants;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Create;

internal sealed class CreateNaturalPersonCommandHandler : IRequestHandler<CreateNaturalPersonCommand, NaturalPersonResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly IPhoneRepository _phoneRepository;
    private readonly ICityRepository _cityRepository;
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public CreateNaturalPersonCommandHandler(
        IUnitOfWork unitOfWork,
        INaturalPersonRepository naturalPersonRepository,
        IPhoneRepository phoneRepository,
        ICityRepository cityRepository,
        IFileService fileService,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _unitOfWork = unitOfWork;
        _naturalPersonRepository = naturalPersonRepository;
        _phoneRepository = phoneRepository;
        _cityRepository = cityRepository;
        _fileService = fileService;
        _localizer = localizer;
    }

    public async Task<NaturalPersonResponse> Handle(CreateNaturalPersonCommand request, CancellationToken cancellationToken)
    {
        var naturalPersonExists = await _naturalPersonRepository.ExistAsync(
            x => x.PersonalNumber == request.PersonalNumber && x.IsActive,
            cancellationToken);

        if (naturalPersonExists)
        {
            throw new AlreadyExistsException(
                nameof(NaturalPerson),
                nameof(request.PersonalNumber),
                request.PersonalNumber,
                _localizer);
        }

        var cityExists = await _cityRepository.ExistAsync(
            x => x.Id == request.CityId,
            cancellationToken: cancellationToken);

        if (!cityExists)
        {
            throw new NotFoundException(
                nameof(City),
                nameof(request.CityId),
                request.CityId.ToString(),
                _localizer);
        }

        var phones = await GetIfValidPhones(request.Phones, cancellationToken);

        if (request.Relations != null && request.Relations.Any())
        {
            var relatedNaturalPersonsExist = await _naturalPersonRepository.ExistAsync(
                x => request.Relations.Select(y => y.RelatedNaturalPersonId).Contains(x.Id) && x.IsActive,
                cancellationToken);

            if (!relatedNaturalPersonsExist)
            {
                throw new RelatedNaturalPersonNotFoundException(_localizer);
            }
        }

        var relations = request.Relations?.Adapt<IEnumerable<NaturalPersonRelation>>();

        var naturalPerson = NaturalPerson.Create(
            request.FirstName,
            request.LastName,
            request.Gender,
            request.PersonalNumber,
            request.BirthDate,
            request.CityId,
            phones,
            relations);

        await _naturalPersonRepository.AddAsync(naturalPerson, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        var fileName = string.Format(FileNameTemplate.Image, naturalPerson.PersonalNumber);
        var filePath = await _fileService.UploadFile(request.Image, fileName);

        naturalPerson.AddImage(filePath);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return naturalPerson.Adapt<NaturalPersonResponse>();
    }

    private async Task<IEnumerable<Phone>> GetIfValidPhones(
        IEnumerable<CreatePhoneCommand> phones,
        CancellationToken cancellationToken)
    {
        var existingPhones = await _phoneRepository.GetAsync(
            x => phones.Select(y => y.Number).Contains(x.Number),
            asNoTracking: false,
            cancellationToken);

        var phoneBelongsToSomeoneElse = existingPhones
            .Any(x => x.NaturalPersonId != null);

        if (phoneBelongsToSomeoneElse)
        {
            throw new PhoneBelongsToSomeoneElseException(_localizer);
        }

        var newPhones = phones
            .Where(phone =>
                !existingPhones.Select(x => x.Number).Contains(phone.Number))
            .Select(phone => phone.Adapt<Phone>())
            .ToList();

        newPhones.AddRange(existingPhones);

        return newPhones;
    }
}
