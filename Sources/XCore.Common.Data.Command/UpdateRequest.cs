namespace XCore.Common.Data.Command;

/// <summary>
///     The update request.
/// </summary>
public class UpdateRequest<TEntity> : RequestBuilder<TEntity>, IRequest<TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="InsertRequest{TEntity}" /> class.
    /// </summary>
    public UpdateRequest()
    {
    }
}