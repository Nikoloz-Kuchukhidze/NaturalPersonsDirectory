using MediatR;
using NaturalPersonsDirectory.Domain.Enums;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Commands.AddRelation;

public sealed record AddRelationCommand(long RelatedNaturalPersonId, RelationType RelationType) : IRequest
{
    public long NaturalPersonId { get; set; }
}