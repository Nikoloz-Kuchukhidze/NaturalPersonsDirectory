namespace NaturalPersonsDirectory.Domain.Primitives;

public abstract class Entity<TIdentifier> : IEntity
    where TIdentifier : IEquatable<TIdentifier>
{
    public TIdentifier Id { get; protected init; } = default!;
}
