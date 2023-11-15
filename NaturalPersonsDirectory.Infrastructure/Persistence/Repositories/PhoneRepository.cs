using NaturalPersonsDirectory.Application.Infrastructure.Repositories;
using NaturalPersonsDirectory.Domain.Entities;
using NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.Repositories;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Repositories;

internal sealed class PhoneRepository : BaseRepository<Phone, long, ApplicationDbContext>, IPhoneRepository
{
    public PhoneRepository(ApplicationDbContext dbContext) 
        : base(dbContext)
    {
    }
}
