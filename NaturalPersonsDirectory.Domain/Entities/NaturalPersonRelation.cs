using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Domain.Primitives;

namespace NaturalPersonsDirectory.Domain.Entities;

public sealed class NaturalPersonRelation : Entity<long>
{
    internal NaturalPersonRelation(
        RelationType relationType,
        NaturalPerson naturalPerson,
        NaturalPerson relatedNaturalPerson)
    {
        RelationType = relationType;
        NaturalPerson = naturalPerson;
        RelatedNaturalPerson = relatedNaturalPerson;
    }

    public NaturalPersonRelation(
        long relatedNaturalPersonId,
        RelationType relationType)
    {
        RelationType = relationType;
        RelatedNaturalPersonId = relatedNaturalPersonId;
    }

    private NaturalPersonRelation()
    {
        
    }

    public RelationType RelationType { get; private set; }

    public long NaturalPersonId { get; private set; }
    public NaturalPerson NaturalPerson { get; private set; } = null!;

    public long RelatedNaturalPersonId { get; private set; }
    public NaturalPerson RelatedNaturalPerson { get; private set; } = null!;
}
