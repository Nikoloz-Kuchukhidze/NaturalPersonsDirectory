using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string itemName, string itemKey, string keyValue, IStringLocalizer<ExceptionMessages> localizer)
        : base(HttpStatusCode.NotFound, string.Format(localizer[nameof(NotFoundException)], localizer[itemName], itemKey, keyValue))
    {
    }
}
