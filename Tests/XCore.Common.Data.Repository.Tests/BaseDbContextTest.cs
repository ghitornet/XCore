namespace XCore.Common.Data.Repository.Tests;

/// <summary>
///     The base db context test.
/// </summary>
public class BaseDbContextTest
{
    /// <summary>
    ///     When BaseDbContext saves an entity, it must save the CreatedAt field.
    /// </summary>
    [Fact]
    public void BaseDbContext_saves_an_entity_it_must_save_the_CreatedAt_field()
    {
        // ARRANGE
        var provider = new ServiceCollection()
            .AddLogging()
            .AddDbContext<TestDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            })
            .AddRepository<TestDbContext>(setEntityReadyToExport: true, ignoreExportTracking: false)
            .BuildServiceProvider();

        var blogRepository = provider.GetRequiredService<IBlogRepository>();
        var postRepository = provider.GetRequiredService<IPostRepository>();

        var blog = new Blog
        {
            Url = "Url Test Blog"
        };

        var post = new Post("Title Test Post", blog.Id);

        blog.Posts.Add(post);

        // ACT
        blogRepository.Add(blog);
        blogRepository.SaveChanges();

        var blogFromDb = blogRepository.Get().Select(x => new { x.Id, x.Url, x.Posts, x.CreatedAt }).ToList();

        // ASSERT
        blog.Id.Should().NotBe(0);
        blog.CreatedAt.Should().NotBe(default);
    }
}