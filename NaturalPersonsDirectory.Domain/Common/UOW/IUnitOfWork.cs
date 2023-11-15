namespace NaturalPersonsDirectory.Domain.Common.UOW;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
