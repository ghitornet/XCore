namespace XCore.Common.Data.Command;

/// <summary>
///     The update request handler.
/// </summary>
public abstract class UpdateRequestHandler<TEntity>(IRepositoryAsync<TEntity> repository)
    : IRequestHandler<UpdateRequest<TEntity>, TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Handles the update request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public virtual async Task<TEntity[]> Handle(UpdateRequest<TEntity> request, CancellationToken cancellationToken)
    {
        await repository.UpdateRangeAsync(request.Entities, true, cancellationToken: cancellationToken);
        return request.Entities;
    }
}