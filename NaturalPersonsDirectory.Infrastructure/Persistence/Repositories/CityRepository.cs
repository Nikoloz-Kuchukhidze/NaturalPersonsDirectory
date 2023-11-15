using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.Repositories;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Repositories;

internal sealed class CityRepository : BaseRepository<City, int, ApplicationDbContext>, ICityRepository
{
    public CityRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}
