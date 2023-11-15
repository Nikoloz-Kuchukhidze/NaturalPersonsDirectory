using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public sealed class RelationAlreadyExistsException : BaseException
{
    public RelationAlreadyExistsException(long naturalPersonId, long relatedNaturalPersonId, IStringLocalizer<ExceptionMessages> localizer)
        : base(HttpStatusCode.Conflict, string.Format(localizer[nameof(RelationAlreadyExistsException)], naturalPersonId, relatedNaturalPersonId))
    {
    }
}
