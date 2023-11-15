using NaturalPersonsDirectory.Domain.Common.Utils;

namespace NaturalPersonsDirectory.Application.Common.Utils;

public class DateTimeProvider : IDateTimeProvider
{
    private readonly TimeZoneInfo _timeZoneInfo;

    public DateTimeProvider(string timeZoneId)
    {
        _timeZoneInfo = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
    }

    public DateTimeOffset Now => TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, _timeZoneInfo);
}
