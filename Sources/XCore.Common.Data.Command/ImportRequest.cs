namespace XCore.Common.Data.Command;

/// <summary>
///     The import request.
/// </summary>
public class ImportRequest<TEntity> : RequestBuilder<TEntity>, IRequest<TEntity[]>
    where TEntity : class
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="ImportRequest{TEntity}" /> class.
    /// </summary>
    public ImportRequest()
    {
    }
}