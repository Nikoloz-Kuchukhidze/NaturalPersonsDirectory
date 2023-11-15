using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage.Constants;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.UploadImage;

internal sealed class UploadNaturalPersonImageCommandHandler : IRequestHandler<UploadNaturalPersonImageCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public UploadNaturalPersonImageCommandHandler(
        IUnitOfWork unitOfWork,
        INaturalPersonRepository naturalPersonRepository,
        IFileService fileService,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _unitOfWork = unitOfWork;
        _naturalPersonRepository = naturalPersonRepository;
        _fileService = fileService;
        _localizer = localizer;
    }

    public async Task Handle(UploadNaturalPersonImageCommand request, CancellationToken cancellationToken)
    {
        var naturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id && x.IsActive,
            asNoTracking: false,
            cancellationToken);

        if (naturalPerson is null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.Id),
                request.Id.ToString(),
                _localizer);
        }

        var fileName = string.Format(FileNameTemplate.Image, naturalPerson.PersonalNumber);
        var filePath = naturalPerson.Image;

        if (string.IsNullOrWhiteSpace(filePath))
        {
            filePath = await _fileService.UploadFile(request.Image, fileName);
        }
        else
        {
            filePath = await _fileService.ReplaceFile(filePath, request.Image);
        }

        naturalPerson.AddImage(filePath);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
