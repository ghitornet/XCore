namespace XCore.Common.Data.Repository;

/// <summary>
///     The repository.
/// </summary>
public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : class, IEntityId
{
    private readonly ILogger<Repository<TDbContext, TEntity>>? _logger;

    /// <summary>
    ///     Initializes a new instance of the <see cref="Repository{TDbContext, TEntity}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="loggerFactory">The logger factory.</param>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
    protected Repository(TDbContext context, ILoggerFactory loggerFactory, bool? setEntityReadyToExport = null, bool? ignoreExportTracking = null)
    {
        Context = context;
        _logger = loggerFactory.CreateLogger<Repository<TDbContext, TEntity>>();
        SetEntityReadyToExport = setEntityReadyToExport;
        IgnoreExportTracking = ignoreExportTracking;
    }

    /// <summary>
    ///     Gets a value indicating whether ignore export tracking.
    /// </summary>
    protected bool? IgnoreExportTracking { get; }

    /// <summary>
    ///     Gets a value indicating whether set entity ready to export.
    /// </summary>
    protected bool? SetEntityReadyToExport { get; }

    /// <summary>
    ///     Gets the context.
    /// </summary>
    protected TDbContext Context { get; }

    /// <inheritdoc />
    public IQueryable<TEntity> Get()
    {
        return Context.Set<TEntity>();
    }

    /// <inheritdoc />
    public TEntity? Get(int id)
    {
        return Context.Set<TEntity>().Find(id);
    }

    /// <inheritdoc />
    public TEntity Add(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        _logger?.LogTrace("Adding entity of type {0} with id {1} to the context.", typeof(TEntity).Name, entity.Id);
        Context.Set<TEntity>().Add(entity);
        if (saveChanges)
        {
            _logger?.LogTrace("Saving changes to the context.");
            SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }

        return entity;
    }

    /// <inheritdoc />
    public void AddRange(IEnumerable<TEntity> entities, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        _logger?.LogTrace("Adding entities of type {0} to the context.", typeof(TEntity).Name);
        Context.Set<TEntity>().AddRange(entities);
        if (saveChanges)
        {
            _logger?.LogTrace("Saving changes to the context.");
            SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }
    }

    /// <inheritdoc />
    public TEntity Update(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        _logger?.LogTrace("Updating entity of type {0} with id {1} to the context.", typeof(TEntity).Name, entity.Id);
        Context.Set<TEntity>().Update(entity);
        if (saveChanges)
        {
            _logger?.LogTrace("Saving changes to the context.");
            SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }

        return entity;
    }

    /// <inheritdoc />
    public void UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        _logger?.LogTrace("Updating entities of type {0} to the context.", typeof(TEntity).Name);
        Context.Set<TEntity>().UpdateRange(entities);
        if (saveChanges)
        {
            _logger?.LogTrace("Saving changes to the context.");
            SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }
    }

    /// <inheritdoc />
    public void Delete(int id, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entity = Context.Set<TEntity>().Find(id);
        if (entity != null)
        {
            _logger?.LogTrace("Deleting entity of type {0} with id {1} from the context.", typeof(TEntity).Name, entity.Id);
            Context.Set<TEntity>().Remove(entity);
            if (saveChanges)
            {
                _logger?.LogTrace("Saving changes to the context.");
                SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
            }
        }
    }

    /// <inheritdoc />
    public void DeleteRange(IEnumerable<int> ids, bool saveChanges = false, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entities = Context.Set<TEntity>().Where(e => ids.Contains(e.Id));
        _logger?.LogTrace("Deleting entities of type {0} from the context.", typeof(TEntity).Name);
        Context.Set<TEntity>().RemoveRange(entities);
        if (saveChanges)
        {
            _logger?.LogTrace("Saving changes to the context.");
            SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }
    }

    /// <inheritdoc />
    public IDbContextTransaction BeginTransaction()
    {
        _logger?.LogTrace("Starting a new transaction.");
        return Context.Database.BeginTransaction();
    }

    /// <inheritdoc />
    public void CommitTransaction(IDbContextTransaction transaction)
    {
        _logger?.LogTrace("Committing the transaction.");
        transaction.Commit();
    }

    /// <inheritdoc />
    public void RollbackTransaction(IDbContextTransaction transaction)
    {
        _logger?.LogTrace("Rolling back the transaction.");
        transaction.Rollback();
    }

    /// <inheritdoc />
    public int SaveChanges(bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var set = setEntityReadyToExport ?? SetEntityReadyToExport;

        return Context.SaveAllChanges(false, set!.Value);
    }

    /// <inheritdoc />
    public int SaveImportChanges(bool? ignoreExportTracking = null)
    {
        if (IgnoreExportTracking is null && ignoreExportTracking is null)
            throw new ArgumentNullException(nameof(ignoreExportTracking), "Parameter not configured correctly.");

        var set = ignoreExportTracking ?? IgnoreExportTracking;

        return Context.SaveImportChanges(set!.Value);
    }

    /// <inheritdoc />
    public int ResetExportEntity(List<object> entitiesExported)
    {
        return Context.SaveExportChanges(entitiesExported);
    }
}