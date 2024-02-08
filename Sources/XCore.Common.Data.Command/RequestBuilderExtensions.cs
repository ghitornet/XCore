namespace XCore.Common.Data.Command;

/// <summary>
///     The request builder extensions.
/// </summary>
public static class RequestBuilderExtensions
{
    /// <summary>
    ///     Adds the entity.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="bom">The bom.</param>
    /// <returns>A RequestBuilder.</returns>
    public static RequestBuilder<TEntity> Add<TEntity>(this RequestBuilder<TEntity> builder,
        TEntity bom)
        where TEntity : class
    {
        builder.Entities = [.. builder.Entities, bom];
        return builder;
    }

    /// <summary>
    ///     Adds the entities range.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="entities">The entities.</param>
    /// <returns>A RequestBuilder.</returns>
    public static RequestBuilder<TEntity> AddRange<TEntity>(this RequestBuilder<TEntity> builder,
        IEnumerable<TEntity> entities)
        where TEntity : class
    {
        foreach (var entity in entities) builder.Entities = [.. builder.Entities, entity];
        return builder;
    }

    /// <summary>
    ///     Inserts the build.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>An IRequest.</returns>
    public static InsertRequest<TEntity> InsertRequest<TEntity>(this RequestBuilder<TEntity> builder)
        where TEntity : class
    {
        return new InsertRequest<TEntity> { Entities = builder.Entities };
    }

    /// <summary>
    ///     Updates the request.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>An UpdateRequest.</returns>
    public static UpdateRequest<TEntity> UpdateRequest<TEntity>(this RequestBuilder<TEntity> builder)
        where TEntity : class
    {
        return new UpdateRequest<TEntity> { Entities = builder.Entities };
    }

    /// <summary>
    ///     Deletes the request.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <returns>A DeleteRequest.</returns>
    public static DeleteRequest<TEntity> DeleteRequest<TEntity>(this RequestBuilder<TEntity> builder)
        where TEntity : class
    {
        return new DeleteRequest<TEntity> { Entities = builder.Entities };
    }
}