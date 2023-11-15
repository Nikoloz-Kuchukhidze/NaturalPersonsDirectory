using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.Repositories;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Repositories;

internal sealed class NaturalPersonRelationRepository 
    : BaseRepository<NaturalPersonRelation, long, ApplicationDbContext>, INaturalPersonRelationRepository
{
    public NaturalPersonRelationRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}
