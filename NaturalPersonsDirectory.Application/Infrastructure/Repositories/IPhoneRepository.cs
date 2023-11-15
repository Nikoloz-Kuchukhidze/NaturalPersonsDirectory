using NaturalPersonsDirectory.Domain.Common.Repositories;
using NaturalPersonsDirectory.Domain.Entities;

namespace NaturalPersonsDirectory.Application.Infrastructure.Repositories;

public interface IPhoneRepository : IRepository<Phone, long>
{
}
