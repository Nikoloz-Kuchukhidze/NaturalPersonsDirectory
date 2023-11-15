using NaturalPersonsDirectory.Domain.Common.Paging;
using NaturalPersonsDirectory.Domain.Primitives;
using System.Linq.Expressions;

namespace NaturalPersonsDirectory.Domain.Common.Repositories;

public interface IRepository<TEntity, TIdentifier>
    where TEntity : Entity<TIdentifier>
    where TIdentifier : IEquatable<TIdentifier>
{
    Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes);

    Task<IPagedList<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, int? page = null, int? pageSize = null, bool asNoTracking = true, CancellationToken cancellationToken = default);
    
    Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default);
    
    Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
}
