namespace NaturalPersonsDirectory.Domain.Primitives;

public interface IAuditableEntity
{
    public DateTimeOffset CreatedOn { get; }
    public DateTimeOffset? UpdatedOn { get; }
}
