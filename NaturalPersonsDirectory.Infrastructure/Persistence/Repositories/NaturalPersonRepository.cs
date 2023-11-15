using Microsoft.EntityFrameworkCore;
using NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;
using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.Repositories;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Repositories;

internal sealed class NaturalPersonRepository
    : BaseRepository<NaturalPerson, long, ApplicationDbContext>, INaturalPersonRepository
{
    public NaturalPersonRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public Task<IEnumerable<NaturalPersonWithRelationsCountResponse>> GetNaturalPersonsWithRelationsCount(CancellationToken cancellationToken = default)
    {
        var data = _dbContext.Set<NaturalPerson>()
            .AsNoTracking()
            .Include(x => x.Relations)
            .IgnoreAutoIncludes()
            .Select(x => new { x.Id, x.Relations })
            .AsEnumerable()
            .Select(x => new NaturalPersonWithRelationsCountResponse(
                x.Id,
                x.Relations
                    .GroupBy(x => x.RelationType)
                    .ToDictionary(g => g.Key, g => g.Count())));

        return Task.FromResult(data);
    }
}
