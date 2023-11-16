using Microsoft.EntityFrameworkCore;
using NaturalPersonsDirectory.Domain.Common.Paging;
using NaturalPersonsDirectory.Domain.Common.Repositories;
using NaturalPersonsDirectory.Domain.Primitives;
using NaturalPersonsDirectory.Infrastructure.Persistence.Extensions;
using System.Linq.Expressions;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.Repositories;

public class BaseRepository<TEntity, TIdentifier, TDbContext> : IRepository<TEntity, TIdentifier>
    where TEntity : Entity<TIdentifier>
    where TIdentifier : IEquatable<TIdentifier>
    where TDbContext : DbContext
{
    protected readonly TDbContext _dbContext;

    public BaseRepository(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _dbContext.AddAsync(entity, cancellationToken);
    }

    public Task RemoveAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbContext.Remove(entity);

        return Task.CompletedTask;
    }

    public async Task<bool> ExistAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<TEntity>()
            .AnyAsync(predicate, cancellationToken);
    }

    public async Task<IPagedList<TEntity>> GetWithPagingAsync(Expression<Func<TEntity, bool>> predicate, int? page = null, int? pageSize = null, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        var data = _dbContext
                .Set<TEntity>()
                .AsNoTracking(asNoTracking)
                .Where(predicate);

        return await PagedList<TEntity>
            .Create(data, page, pageSize);
    }

    public async Task<TEntity?> GetSingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includes)
    {
         var query = _dbContext
            .Set<TEntity>()
            .AsNoTracking(asNoTracking);

        foreach(var include in includes)
        {
            query = query.Include(include);
        }

        return await query.SingleOrDefaultAsync(predicate, cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate, bool asNoTracking = true, CancellationToken cancellationToken = default)
    {
        return await _dbContext
            .Set<TEntity>()
            .AsNoTracking(asNoTracking)
            .Where(predicate)
            .ToListAsync();
    }
}
