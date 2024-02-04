namespace XCore.Common.Data.Entity.Tests;

/// <summary>
///     The post.
/// </summary>
[Context("TestDbContext")]
[Index(nameof(RowGuid), IsUnique = true)]
public class Post : EntityBase
{
    /// <summary>
    ///     Initializes a new instance of the <see cref="Post" /> class.
    /// </summary>
    public Post()
    {
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="Post" /> class.
    /// </summary>
    /// <param name="title">The title.</param>
    /// <param name="blogId">The blog id.</param>
    [SetsRequiredMembers]
    public Post(string title, int blogId)
    {
        Title = title;
        BlogId = blogId;
    }

    /// <summary>
    ///     Gets or sets the title.
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    ///     Gets or sets the content.
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    ///     Gets or sets the blog id.
    /// </summary>
    public required int BlogId { get; set; }

    /// <summary>
    ///     Gets or sets the blog.
    /// </summary>
    public Blog? Blog { get; set; }
}