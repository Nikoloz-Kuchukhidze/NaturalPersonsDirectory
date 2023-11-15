using NaturalPersonsDirectory.Application.Features.NaturalPersons.Queries.GetRelationsCount;
using NaturalPersonsDirectory.Domain.Common.Repositories;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Infrastructure.Repositories;

public interface INaturalPersonRepository : IRepository<NaturalPerson, long>
{
    Task<IEnumerable<NaturalPersonWithRelationsCountResponse>> GetNaturalPersonsWithRelationsCount(CancellationToken cancellationToken = default);
}
