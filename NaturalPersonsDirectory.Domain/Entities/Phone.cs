using NaturalPersonsDirectory.Domain.Enums;
using NaturalPersonsDirectory.Domain.Primitives;

namespace NaturalPersonsDirectory.Domain.Entities;

public sealed class Phone : Entity<long>
{
    public PhoneType Type { get; private set; }
    public string Number { get; private set; } = null!;

    public long? NaturalPersonId { get; private set; }
    public NaturalPerson? NaturalPerson { get; private set; }
}
