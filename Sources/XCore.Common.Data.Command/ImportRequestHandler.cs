namespace XCore.Common.Data.Command;

/// <summary>
///     The import request handler.
/// </summary>
public abstract class ImportRequestHandler<TEntity>(IRepositoryAsync<TEntity> repository)
    : IRequestHandler<ImportRequest<TEntity>, TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Handles the insert request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public virtual async Task<TEntity[]> Handle(ImportRequest<TEntity> request, CancellationToken cancellationToken)
    {
        try
        {
            await repository.AddRangeAsync(request.Entities, cancellationToken: cancellationToken);
            await repository.SaveImportChangesAsync(cancellationToken: cancellationToken);
            return request.Entities;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}