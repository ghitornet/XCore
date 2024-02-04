namespace XCore.Common.Data.Entity.Tests;

/// <summary>
///     The blog.
/// </summary>
[Context("TestDbContext")]
[Index(nameof(RowGuid), IsUnique = true)]
public class Blog : EntityBase
{
    /// <summary>
    ///     Gets or sets the url.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    ///     Gets the posts.
    /// </summary>
    public List<Post> Posts { get; } = [];
}