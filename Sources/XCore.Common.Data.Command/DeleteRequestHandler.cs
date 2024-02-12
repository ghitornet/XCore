namespace XCore.Common.Data.Command;

/// <summary>
///     The delete request handler.
/// </summary>
public abstract class DeleteRequestHandler<TEntity>(IRepositoryAsync<TEntity> repository)
    : IRequestHandler<DeleteRequest<TEntity>, TEntity[]>
    where TEntity : class, IEntityId
{
    /// <summary>
    ///     Handles the update request.
    /// </summary>
    /// <param name="request">The request.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A Task.</returns>
    public async Task<TEntity[]> Handle(DeleteRequest<TEntity> request, CancellationToken cancellationToken)
    {
        await repository.DeleteRangeAsync(request.Entities.Select(x => x.Id), true, cancellationToken: cancellationToken);
        return request.Entities;
    }
}