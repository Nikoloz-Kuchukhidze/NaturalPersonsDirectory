using Microsoft.EntityFrameworkCore;
using NaturalPersonsDirectory.Domain.Primitives;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Extensions;

public static class IQueryableExtensions
{
    public static IQueryable<TEntity> AsNoTracking<TEntity>(this IQueryable<TEntity> source, bool asNoTracking)
        where TEntity : class, IEntity
    {
        return asNoTracking
            ? source.AsNoTracking()
            : source.AsTracking();
    }
}
