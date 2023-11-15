using NaturalPersonsDirectory.Domain.Common.Repositories;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Infrastructure.Repositories;

public interface ICityRepository : IRepository<City, int>
{
}
