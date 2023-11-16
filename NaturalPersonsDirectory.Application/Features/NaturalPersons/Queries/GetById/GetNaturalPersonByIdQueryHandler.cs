using Mapster;
using MediatR;
using Microsoft.Extensions.Localization;
using NaturalPersonsDirectory.Application.Common.Exceptions;
using NaturalPersonsDirectory.Application.Common.Resources;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Shared;
using NaturalPersonsDirectory.Application.Infrastructure.FileStorage;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;
using System.Linq.Expressions;

namespace NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetById;

internal sealed class GetNaturalPersonByIdQueryHandler : IRequestHandler<GetNaturalPersonByIdQuery, NaturalPersonResponse>
{
    private readonly INaturalPersonRepository _naturalPersonRepository;
    private readonly IFileService _fileService;
    private readonly IStringLocalizer<ExceptionMessages> _localizer;

    public GetNaturalPersonByIdQueryHandler(
        INaturalPersonRepository naturalPersonRepository,
        IFileService fileService,
        IStringLocalizer<ExceptionMessages> localizer)
    {
        _naturalPersonRepository = naturalPersonRepository;
        _fileService = fileService;
        _localizer = localizer;
    }

    public async Task<NaturalPersonResponse> Handle(GetNaturalPersonByIdQuery request, CancellationToken cancellationToken)
    {
        var naturalPerson = await _naturalPersonRepository.GetSingleOrDefaultAsync(
            x => x.Id == request.Id && x.IsActive,
            cancellationToken: cancellationToken,
            includes: new Expression<Func<NaturalPerson, object>>[]
            {
                x => x.City!,
                x => x.Phones,
                x => x.Relations
            });

        if (naturalPerson is null)
        {
            throw new NotFoundException(
                nameof(NaturalPerson),
                nameof(request.Id),
                request.Id.ToString(),
                _localizer);
        }

        var response = naturalPerson.Adapt<NaturalPersonResponse>();
        response.Image = await _fileService.GetFilePublicUrl(naturalPerson.Image!);

        return response;
    }
}
