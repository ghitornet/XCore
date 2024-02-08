namespace XCore.Common.Data.Command;

/// <summary>
///     The delete request.
/// </summary>
public class DeleteRequest<TEntity> : RequestBuilder<TEntity>, IRequest<TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InsertRequest{TEntity}" /> class.
    /// </summary>
    public DeleteRequest()
    {
    }
}