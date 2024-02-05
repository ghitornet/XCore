namespace XCore.Common.Data.Repository;

/// <summary>
///     The repository async.
/// </summary>
public interface IRepositoryAsync<TEntity> : IRepository<TEntity>
{
    /// <summary>
    ///     Gets entity async.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A TEntity? .</returns>
    Task<TEntity?> GetAsync(int id, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds the entity async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The entity added.
    /// </returns>
    Task<TEntity> AddAsync(TEntity entity, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Adds the entities range async.
    /// </summary>
    /// <param name="entities">
    ///     The entities.
    /// </param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    Task AddRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the entity async.
    /// </summary>
    /// <param name="entity">The entity.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The entity added.
    /// </returns>
    Task<TEntity> UpdateAsync(TEntity entity, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Updates the entities range async.
    /// </summary>
    /// <param name="entities">The entities.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task UpdateRangeAsync(IEnumerable<TEntity> entities, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes the entity async.
    /// </summary>
    /// <param name="id">The id.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task DeleteAsync(int id, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Deletes the entities range async.
    /// </summary>
    /// <param name="ids">The ids.</param>
    /// <param name="saveChanges">if set to <c>true</c> [save changes].</param>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task DeleteRangeAsync(IEnumerable<int> ids, bool saveChanges = true, bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Begins the transaction async.
    /// </summary>
    /// <returns>A Task.</returns>
    Task<IDbContextTransaction> BeginTransactionAsync();

    /// <summary>
    ///     Commits the transaction async.
    /// </summary>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A Task.</returns>
    Task CommitTransactionAsync(IDbContextTransaction transaction);

    /// <summary>
    ///     Rollbacks the transaction async.
    /// </summary>
    /// <param name="transaction">The transaction.</param>
    /// <returns>A Task.</returns>
    Task RollbackTransactionAsync(IDbContextTransaction transaction);

    /// <summary>
    ///     Saves the changes async.
    /// </summary>
    /// <param name="setEntityReadyToExport">if set to <c>true</c> [set entity ready to export].</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    Task<int> SaveChangesAsync(bool? setEntityReadyToExport = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Saves the import changes Async.
    /// </summary>
    /// <remarks>
    ///     Saves the import changes.
    /// </remarks>
    /// <param name="ignoreExportTracking">
    ///     If true, ignore export tracking.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The number of state entries written to the database.
    /// </returns>
    Task<int> SaveImportChangesAsync(bool? ignoreExportTracking = null, CancellationToken cancellationToken = default);

    /// <summary>
    ///     Resets the export entity Async.
    /// </summary>
    /// <remarks>
    ///     Resets the export entity.
    /// </remarks>
    /// <param name="entitiesExported">
    ///     The entities exported.
    /// </param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>
    ///     The number of state entries written to the database.
    /// </returns>
    Task<int> ResetExportEntityAsync(List<object> entitiesExported, CancellationToken cancellationToken = default);
}