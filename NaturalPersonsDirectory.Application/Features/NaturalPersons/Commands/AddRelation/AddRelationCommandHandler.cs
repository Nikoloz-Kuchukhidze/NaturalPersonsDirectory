using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.AddRelation;

internal sealed class AddRelationCommandHandler : IRequestHandler<AddRelationCommand>
{
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public AddRelationCommandHandler(
        INaturalPersonRepository naturalPersonRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _naturalPersonRepository = naturalPersonRepository;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task Handle(AddRelationCommand request, CancellationToken cancellationToken)
    {
        var naturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.NaturalPersonId && x.IsActive,
            asNoTracking: false,
            cancellationToken,
            includes: x => x.Relations);

        if(naturalPerson == null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.NaturalPersonId),
                request.NaturalPersonId.ToString(),
                _localizer);
        }

        var relatedNaturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.RelatedNaturalPersonId && x.IsActive,
            asNoTracking: false,
            cancellationToken);

        if (relatedNaturalPerson == null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.RelatedNaturalPersonId),
                request.RelatedNaturalPersonId.ToString(),
                _localizer);
        }

        if (naturalPerson.Relations.Any(x => x.RelatedNaturalPersonId == relatedNaturalPerson.Id))
        {
            throw new RelationAlreadyExistsException(
                naturalPerson.Id, 
                relatedNaturalPerson.Id,
                _localizer);
        }

        naturalPerson.AddRelation(relatedNaturalPerson, request.RelationType);

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
