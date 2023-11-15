using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Common.UOW;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.RemoveRelation;

internal sealed class RemoveRelationCommandHandler : IRequestHandler<RemoveRelationCommand>
{
    private readonly INaturalPersonRelationRepository _naturalPersonRelationRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public RemoveRelationCommandHandler(
        INaturalPersonRelationRepository naturalPersonRelationRepository,
        IUnitOfWork unitOfWork,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _naturalPersonRelationRepository = naturalPersonRelationRepository;
        _unitOfWork = unitOfWork;
        _localizer = localizer;
    }

    public async Task Handle(RemoveRelationCommand request, CancellationToken cancellationToken)
    {
        var relation = await _naturalPersonRelationRepository.GetSingleOrDefaultAsync(
            x => x.NaturalPersonId == request.NaturalPersonId &&
                x.RelatedNaturalPersonId == request.RelatedNaturalPersonId,
            asNoTracking: false,
            cancellationToken);

        if(relation is null) 
        {
            throw new NoRelationException(
                request.NaturalPersonId, 
                request.RelatedNaturalPersonId,
                _localizer);
        }

        await _naturalPersonRelationRepository.RemoveAsync(relation, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
