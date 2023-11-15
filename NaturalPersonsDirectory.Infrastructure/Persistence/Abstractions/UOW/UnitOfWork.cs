using Microsoft.EntityFrameworkCore;
using NaturalPersonsDirectory.Domain.Common.UOW;
using NaturalPersonsDirectory.Domain.Common.Utils;
using NaturalPersonsDirectory.Domain.Primitives;
using NaturalPersonsDirectory.Infrastructure.Persistence.Contexts;

namespace NaturalPersonsDirectory.Infrastructure.Persistence.Abstractions.UOW;

internal sealed class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UnitOfWork(ApplicationDbContext dbContext, IDateTimeProvider dateTimeProvider)
    {
        _dbContext = dbContext;
        _dateTimeProvider = dateTimeProvider;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditableEntities();

        return _dbContext.SaveChangesAsync(cancellationToken);
    }

    private void UpdateAuditableEntities()
    {
        var entries = _dbContext
            .ChangeTracker
            .Entries<IAuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Property(a => a.CreatedOn).CurrentValue = _dateTimeProvider.Now;
            }

            if (entry.State == EntityState.Modified)
            {
                entry.Property(a => a.UpdatedOn).CurrentValue = _dateTimeProvider.Now;
            }
        }
    }
}
