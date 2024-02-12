namespace XCore.Common.Data.Repository;

/// <summary>
///     The repository async.
/// </summary>
public abstract class RepositoryAsync<TDbContext, TEntity> : Repository<TDbContext, TEntity>, IRepositoryAsync<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : class, IEntityId
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="RepositoryAsync{TDbContext, TEntity}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
    protected RepositoryAsync(TDbContext context, ILoggerFactory loggerFactory, bool? setEntityReadyToExport = null,
        bool? ignoreExportTracking = null) : base(context, loggerFactory, setEntityReadyToExport, ignoreExportTracking)
    {
    }

    /// <inheritdoc />
    public async Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        return await Context.Set<TEntity>().FindAsync([id], cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> AddAsync(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        await Context.Set<TEntity>().AddAsync(entity, cancellationToken);
        if (saveChanges) await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);

        return entity;
    }

    /// <inheritdoc />
    public async Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false,
        bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        await Context.Set<TEntity>().AddRangeAsync(entities, cancellationToken);
        if (saveChanges) await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().Update(entity);
        if (saveChanges) await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);

        return entity;
    }

    /// <inheritdoc />
    public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = false,
        bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().UpdateRange(entities);
        if (saveChanges) await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id, bool saveChanges = false, bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entity = await Context.Set<TEntity>().FindAsync([id], cancellationToken);
        if (entity != null)
        {
            Context.Set<TEntity>().Remove(entity);
            if (saveChanges)
                await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);
        }
    }

    /// <inheritdoc />
    public async Task DeleteRangeAsync(IEnumerable<int> ids, bool saveChanges = false,
        bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entities = Context.Set<TEntity>().Where(e => ids.Contains(e.Id));
        Context.Set<TEntity>().RemoveRange(entities);
        if (saveChanges) await SaveChangesAsync(setEntityReadyToExport ?? SetEntityReadyToExport, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<IDbContextTransaction> BeginTransactionAsync()
    {
        return await Context.Database.BeginTransactionAsync();
    }

    /// <inheritdoc />
    public async Task CommitTransactionAsync(IDbContextTransaction transaction)
    {
        await transaction.CommitAsync();
    }

    /// <inheritdoc />
    public async Task RollbackTransactionAsync(IDbContextTransaction transaction)
    {
        await transaction.RollbackAsync();
    }

    /// <inheritdoc />
    public async Task<int> SaveChangesAsync(bool? setEntityReadyToExport = null,
        CancellationToken cancellationToken = default)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var set = setEntityReadyToExport ?? SetEntityReadyToExport;

        return await Context.SaveAllChangesAsync(false, set!.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<int> SaveImportChangesAsync(bool? ignoreExportTracking = null,
        CancellationToken cancellationToken = default)
    {
        if (IgnoreExportTracking is null && ignoreExportTracking is null)
            throw new ArgumentNullException(nameof(ignoreExportTracking), "Parameter not configured correctly.");

        var set = ignoreExportTracking ?? IgnoreExportTracking;

        return await Context.SaveImportChangesAsync(set!.Value, cancellationToken);
    }

    /// <inheritdoc />
    public async Task<int> ResetExportEntityAsync(List<object> entitiesExported,
        CancellationToken cancellationToken = default)
    {
        return await Context.SaveExportChangesAsync(entitiesExported, cancellationToken);
    }
}