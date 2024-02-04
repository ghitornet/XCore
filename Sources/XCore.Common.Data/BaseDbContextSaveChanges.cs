namespace XCore.Common.Data;

/// <summary>
///     The base db context.
/// </summary>
public abstract partial class BaseDbContext
{
    /// <summary>
    ///     Saves the changes async.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the changes to the database asynchronously.
    ///     if the entity is marked as IEntityExportTracking and setEntityReadyToExport is true, it will set the entity as
    ///     ready to export.
    /// </remarks>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <param name="setEntityReadyToExport">
    ///     If true, set entity ready to export.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveAllChangesAsync(bool acceptAllChangesOnSuccess, bool setEntityReadyToExport,
        CancellationToken cancellationToken = default)
    {
        var entityEntries = BeforeSaveChanges(setEntityReadyToExport);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the import changes async.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the import changes to the database asynchronously.
    ///     if ignoreExportTracking is true, it will ignore all entities that are marked as
    ///     <see cref="IEntityExportTracking.IsReadyToExport" /> = true.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveImportChangesAsync(bool acceptAllChangesOnSuccess, bool ignoreExportTracking,
        CancellationToken cancellationToken = default)
    {
        var entityEntries = BeforeSaveImportChanges(ignoreExportTracking);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the export changes async.
    /// </summary>
    /// <remarks>
    ///     This method only saves the exported state for the entities that are passed.
    ///     If there are other changes in the data context, they are erased before the data is saved.
    /// </remarks>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveExportChangesAsync(List<object> entitiesExported, bool acceptAllChangesOnSuccess,
        CancellationToken cancellationToken = default)
    {
        ChangeTracker.Clear();
        BeforeSaveExportChanges(entitiesExported);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the changes.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the changes to the database.
    ///     if the entity is marked as IEntityExportTracking and setEntityReadyToExport is true, it will set the entity as
    ///     ready to export.
    /// </remarks>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <param name="setEntityReadyToExport">
    ///     If true, set entity ready to export.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveAllChanges(bool acceptAllChangesOnSuccess, bool setEntityReadyToExport)
    {
        var entityEntries = BeforeSaveChanges(setEntityReadyToExport);
        OnBeforeSaveChanges();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the import changes.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the import changes to the database.
    ///     if ignoreExportTracking is true, it will ignore all entities that are marked as
    ///     <see cref="IEntityExportTracking.IsReadyToExport" /> = true.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveImportChanges(bool acceptAllChangesOnSuccess, bool ignoreExportTracking)
    {
        var entityEntries = BeforeSaveImportChanges(ignoreExportTracking);
        OnBeforeSaveChanges();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the export changes.
    /// </summary>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <param name="acceptAllChangesOnSuccess">
    ///     If true, accept all changes on success.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveExportChanges(List<object> entitiesExported, bool acceptAllChangesOnSuccess)
    {
        BeforeSaveExportChanges(entitiesExported);
        OnBeforeSaveChanges();
        var result = base.SaveChanges(acceptAllChangesOnSuccess);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the changes.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the changes to the database.
    ///     if the entity is marked as IEntityExportTracking and setEntityReadyToExport is true, it will set the entity as
    ///     ready to export.
    /// </remarks>
    /// <param name="setEntityReadyToExport">
    ///     If true, set entity ready to export.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveAllChanges(bool setEntityReadyToExport)
    {
        var entityEntries = BeforeSaveChanges(setEntityReadyToExport);
        OnBeforeSaveChanges();
        var result = base.SaveChanges();
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the import changes.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the import changes to the database.
    ///     if ignoreExportTracking is true, it will ignore all entities that are marked as
    ///     <see cref="IEntityExportTracking.IsReadyToExport" /> = true.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveImportChanges(bool ignoreExportTracking)
    {
        var entityEntries = BeforeSaveImportChanges(ignoreExportTracking);
        OnBeforeSaveChanges();
        var result = base.SaveChanges();
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the export changes.
    /// </summary>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <returns>An int.</returns>
    public int SaveExportChanges(List<object> entitiesExported)
    {
        BeforeSaveExportChanges(entitiesExported);
        OnBeforeSaveChanges();
        var result = base.SaveChanges();
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the all changes async.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the changes to the database asynchronously.
    ///     if the entity is marked as IEntityExportTracking and setEntityReadyToExport is true, it will set the entity as
    ///     ready to export.
    /// </remarks>
    /// <param name="setEntityReadyToExport">If true, set entity ready to export.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveAllChangesAsync(bool setEntityReadyToExport,
        CancellationToken cancellationToken = default)
    {
        var entityEntries = BeforeSaveChanges(setEntityReadyToExport);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(cancellationToken);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the import changes async.
    /// </summary>
    /// <remarks>
    ///     This method is used to save the import changes to the database asynchronously.
    ///     if ignoreExportTracking is true, it will ignore all entities that are marked as
    ///     <see cref="IEntityExportTracking.IsReadyToExport" /> = true.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveImportChangesAsync(bool ignoreExportTracking,
        CancellationToken cancellationToken = default)
    {
        var entityEntries = BeforeSaveImportChanges(ignoreExportTracking);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(cancellationToken);
        OnAfterSaveChanges(entityEntries);
        this.ChangeTracker.Clear();
        return result;
    }

    /// <summary>
    ///     Saves the export changes async.
    /// </summary>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <param name="cancellationToken">
    ///     The cancellation token.
    /// </param>
    /// <returns>A Task.</returns>
    public async Task<int> SaveExportChangesAsync(List<object> entitiesExported,
        CancellationToken cancellationToken = default)
    {
        BeforeSaveExportChanges(entitiesExported);
        OnBeforeSaveChanges();
        var result = await base.SaveChangesAsync(cancellationToken);
        this.ChangeTracker.Clear();
        return result;
    }
}