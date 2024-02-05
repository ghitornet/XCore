﻿namespace XCore.Common.Data.Repository;

/// <summary>
///     The repository.
/// </summary>
public abstract class Repository<TDbContext, TEntity> : IRepository<TEntity>
    where TDbContext : BaseDbContext
    where TEntity : class, IEntityId
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Repository{TDbContext, TEntity}" /> class.
    /// </summary>
    /// <param name="context">The context.</param>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="ignoreExportTracking">If true, ignore export tracking.</param>
    protected Repository(TDbContext context, bool? setEntityReadyToExport = null, bool? ignoreExportTracking = null)
    {
        Context = context;
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
    public TEntity Add(TEntity entity, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().Add(entity);
        if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);

        return entity;
    }

    /// <inheritdoc />
    public void AddRange(IEnumerable<TEntity> entities, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().AddRange(entities);
        if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
    }

    /// <inheritdoc />
    public TEntity Update(TEntity entity, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().Update(entity);
        if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);

        return entity;
    }

    /// <inheritdoc />
    public void UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        Context.Set<TEntity>().UpdateRange(entities);
        if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
    }

    /// <inheritdoc />
    public void Delete(int id, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entity = Context.Set<TEntity>().Find(id);
        if (entity != null)
        {
            Context.Set<TEntity>().Remove(entity);
            if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
        }
    }

    /// <inheritdoc />
    public void DeleteRange(IEnumerable<int> ids, bool saveChanges = true, bool? setEntityReadyToExport = null)
    {
        if (SetEntityReadyToExport is null && setEntityReadyToExport is null)
            throw new ArgumentNullException(nameof(setEntityReadyToExport), "Parameter not configured correctly.");

        var entities = Context.Set<TEntity>().Where(e => ids.Contains(e.Id));
        Context.Set<TEntity>().RemoveRange(entities);
        if (saveChanges) SaveChanges(setEntityReadyToExport ?? SetEntityReadyToExport);
    }

    /// <inheritdoc />
    public IDbContextTransaction BeginTransaction()
    {
        return Context.Database.BeginTransaction();
    }

    /// <inheritdoc />
    public void CommitTransaction(IDbContextTransaction transaction)
    {
        transaction.Commit();
    }

    /// <inheritdoc />
    public void RollbackTransaction(IDbContextTransaction transaction)
    {
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