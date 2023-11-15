using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public sealed class AlreadyExistsException : BaseException
{
    public AlreadyExistsException(string itemName, string itemKey, string keyValue, IStringLocalizer<ExceptionMessages> localizer)
        : base(HttpStatusCode.Conflict, string.Format(localizer[nameof(AlreadyExistsException)], localizer[itemName], itemKey, keyValue))
    {
    }
}
