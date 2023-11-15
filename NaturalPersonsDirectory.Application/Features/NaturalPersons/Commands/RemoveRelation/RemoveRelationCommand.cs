using MediatR;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.RemoveRelation;

public record RemoveRelationCommand(
    long NaturalPersonId,
    long RelatedNaturalPersonId) : IRequest;