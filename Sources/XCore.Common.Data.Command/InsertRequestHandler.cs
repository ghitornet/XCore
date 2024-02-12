namespace XCore.Common.Data.Command;

/// <summary>
///     The insert request handler.
/// </summary>
/// <remarks>
///     Initializes a new instance of the <see cref="InsertRequestHandler{TEntity}" /> class.
/// </remarks>
/// <param name="repository">The repository.</param>
public abstract class InsertRequestHandler<TEntity>(IRepositoryAsync<TEntity> repository)
    : IRequestHandler<InsertRequest<TEntity>, TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Handles the insert request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public virtual async Task<TEntity[]> Handle(InsertRequest<TEntity> request, CancellationToken cancellationToken)
    {
        try
        {
            await repository.AddRangeAsync(request.Entities, true, cancellationToken: cancellationToken);
            return request.Entities;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}