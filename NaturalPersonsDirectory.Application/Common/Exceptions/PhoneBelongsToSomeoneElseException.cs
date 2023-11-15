using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public sealed class PhoneBelongsToSomeoneElseException : BaseException
{
    public PhoneBelongsToSomeoneElseException(IStringLocalizer<ExceptionMessages> localizer)
        : base(HttpStatusCode.Conflict, localizer[nameof(PhoneBelongsToSomeoneElseException)])
    {
    }
}
