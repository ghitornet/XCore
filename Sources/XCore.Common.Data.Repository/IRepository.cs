namespace XCore.Common.Data.Repository;

/// <summary>
///     The repository.
/// </summary>
public interface IRepository<TEntity>
{
    /// <summary>
    ///     Gets the query for all entities.
    /// </summary>
    /// <remarks>
    ///     Gets the query for all entities.
    /// </remarks>
    /// <returns>A list of TEntities.</returns>
    IQueryable<TEntity> Get();

    /// <summary>
    ///     Gets entity.
    /// </summary>
    /// <remarks>
    ///     Gets the specified entity.
    /// </remarks>
    /// <param name="id">The id.</param>
    /// <returns>A TEntity? .</returns>
    TEntity? Get(int id);

    /// <summary>
    ///     Adds the entity.
    /// </summary>
    /// <remarks>
    ///     Adds the new entity.
    /// </remarks>
    /// <param name="entity">The entity.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    TEntity Add(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Adds the entities range.
    /// </summary>
    /// <remarks>
    ///     Adds the new entities range.
    /// </remarks>
    /// <param name="entities">The entities.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    void AddRange(IEnumerable<TEntity> entities, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Updates the entity.
    /// </summary>
    /// <remarks>
    ///     Updates the entity.
    /// </remarks>
    /// <param name="entity">The entity.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    TEntity Update(TEntity entity, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Updates the entities range.
    /// </summary>
    /// <remarks>
    ///     Updates the entities range.
    /// </remarks>
    /// <param name="entities">The entities.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    void UpdateRange(IEnumerable<TEntity> entities, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Deletes the entity.
    /// </summary>
    /// <remarks>
    ///     Deletes the entity.
    /// </remarks>
    /// <param name="id">The id.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    void Delete(int id, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Deletes the entities range.
    /// </summary>
    /// <remarks>
    ///     Deletes the entities range.
    /// </remarks>
    /// <param name="ids">The ids.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    void DeleteRange(IEnumerable<int> ids, bool saveChanges = false, bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Begins the transaction.
    /// </summary>
    /// <remarks>
    ///     Begins the transaction.
    /// </remarks>
    /// <returns>An IDbContextTransaction.</returns>
    IDbContextTransaction BeginTransaction();

    /// <summary>
    ///     Commits the transaction.
    /// </summary>
    /// <remarks>
    ///     Commits the transaction.
    /// </remarks>
    /// <param name="transaction">
    ///     The transaction.
    /// </param>
    void CommitTransaction(IDbContextTransaction transaction);

    /// <summary>
    ///     Rollbacks the transaction.
    /// </summary>
    /// <remarks>
    ///     Rollbacks the transaction.
    /// </remarks>
    /// <param name="transaction">
    ///     The transaction.
    /// </param>
    void RollbackTransaction(IDbContextTransaction transaction);

    /// <summary>
    ///     Saves the changes.
    /// </summary>
    /// <remarks>
    ///     Saves the changes.
    /// </remarks>
    /// <param name="setEntityReadyToExport">
    ///     if set to <c>true</c> [set entity ready to export].
    /// </param>
    /// <returns>
    ///     The number of state entries written to the database.
    /// </returns>
    int SaveChanges(bool? setEntityReadyToExport = null);

    /// <summary>
    ///     Saves the import changes.
    /// </summary>
    /// <remarks>
    ///     Saves the import changes.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <returns>
    ///     The number of state entries written to the database.
    /// </returns>
    int SaveImportChanges(bool? ignoreExportTracking = null);

    /// <summary>
    ///     Resets the export entity.
    /// </summary>
    /// <remarks>
    ///     Resets the export entity.
    /// </remarks>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <returns>
    ///     The number of state entries written to the database.
    /// </returns>
    int ResetExportEntity(List<object> entitiesExported);
}