using HCWeb.NET.Database.Interfaces;
using HCWeb.NET.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace HCWeb.NET.Database.Interceptors;

public class SoftDeleteInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null) return result;

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;

            entry.State = EntityState.Modified;
            delete.DeletedAt = DateTimeOffset.UtcNow;
        }

        return result;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new())
    {
        if (eventData.Context is null) return new ValueTask<InterceptionResult<int>>(result);

        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            if (entry is not { State: EntityState.Deleted, Entity: ISoftDelete delete }) continue;

            entry.State = EntityState.Modified;
            delete.DeletedAt = DateTimeOffset.UtcNow;
        }

        return new ValueTask<InterceptionResult<int>>(result);
    }
}