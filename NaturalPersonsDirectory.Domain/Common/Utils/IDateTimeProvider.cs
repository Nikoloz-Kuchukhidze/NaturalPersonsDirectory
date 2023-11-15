namespace NaturalPersonsDirectory.Domain.Common.Utils;

public interface IDateTimeProvider
{
    public DateTimeOffset Now { get; }
}