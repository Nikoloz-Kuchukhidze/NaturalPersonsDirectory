using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Resources;
using System.Net;

namespace NaturalPersonsDirectory.Application.Common.Exceptions;

public sealed class RelatedNaturalPersonNotFoundException : BaseException
{
    public RelatedNaturalPersonNotFoundException(IStringLocalizer<ExceptionMessages> _localizer)
        : base(HttpStatusCode.NotFound, _localizer[nameof(RelatedNaturalPersonNotFoundException)])
    {
    }
}
