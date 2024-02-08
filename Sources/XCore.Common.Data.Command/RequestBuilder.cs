namespace XCore.Common.Data.Command;

/// <summary>
///     The request builder.
/// </summary>
public class RequestBuilder<TEntity>
    where TEntity : class
{
    /// <summary>
    ///     Gets or sets the entity.
    /// </summary>
    public TEntity[] Entities { get; set; } = [];
}