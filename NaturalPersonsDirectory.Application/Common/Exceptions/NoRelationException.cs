using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public sealed class NoRelationException : BaseException
{
    public NoRelationException(long naturalPersonId, long relatedNaturalPersonId, IStringLocalizer<ExceptionMessages> localizer)
        : base(HttpStatusCode.NotFound, string.Format(localizer[nameof(NoRelationException)], relatedNaturalPersonId, naturalPersonId))
    {
    }
}
