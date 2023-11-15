using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.Delete;

internal sealed class DeleteNaturalPersonCommandHandler : IRequestHandler<DeleteNaturalPersonCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public DeleteNaturalPersonCommandHandler(
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

    public async Task Handle(DeleteNaturalPersonCommand request, CancellationToken cancellationToken)
    {
        var naturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id && x.IsActive,
            asNoTracking: false,
            cancellationToken);

        if(naturalPerson is null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.Id),
                request.Id.ToString(),
                _localizer);
        }

        var filePath = naturalPerson.Image;

        naturalPerson.Delete();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        if (!string.IsNullOrWhiteSpace(filePath))
        {
            await _fileService.RemoveFile(filePath);
        }
    }
}
